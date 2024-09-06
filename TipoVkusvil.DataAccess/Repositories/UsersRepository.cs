using Microsoft.EntityFrameworkCore;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Core.Enums;
using TipoVkusvil.DataAccess.Entities;
using TipoVkusvil.Models;

namespace TipoVkusvil.DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly ShopDbContext _context;

    public UsersRepository(ShopDbContext context)
    {
        _context = context;
    }

    public async Task Add(User user)
    {
        var userEntity = new UserEntity()
        {
            Id = user.Id,
            UserName = user.UserName,
            PasswordHash = user.PasswordHash,
            Email = user.Email
        };

        var shopCartEntity = new ShopCartEntity()
        {
            Id = Guid.NewGuid(),
            CartItems = new List<CartItemEntity>(),
            CreatedDate = DateTime.UtcNow.AddHours(3),
            UserId = user.Id
        };
        await _context.Users.AddAsync(userEntity);
        await _context.ShopCarts.AddAsync(shopCartEntity);
        UserRoleEntity temp = new UserRoleEntity()
        {
            UserId = user.Id,
            RoleId = 2
        };
        await _context.AddAsync(temp);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        var userEntity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email) ?? throw new Exception();

        return User.Create(userEntity.Id, userEntity.UserName, userEntity.PasswordHash, userEntity.Email).user;
    }


    public async Task<HashSet<Permission>> GetUserPermissions(Guid userId)
    {
        var sd = await _context.Set<UserRoleEntity>().Where(ur => ur.UserId == userId).ToArrayAsync();

        int[] roles = sd.Select(r => r.RoleId).ToArray();

        int[] permissionId = await _context.Set<RolePermissionEntity>().Where(rp => roles.Contains(rp.RoleId))
            .Select(rp => rp.PermissionId).ToArrayAsync();

        return permissionId.Select(p => (Permission)p).ToHashSet();
        
        
    }
    
    //for admin panel:
    
    
    
    /*public async Task Create(User user)
    {
        var userEntity = new UserEntity()
        {
            Id = user.Id,
            UserName = user.UserName,
            PasswordHash = user.PasswordHash,
            Email = user.Email
        };

        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
    }
    */

    public async Task<Guid> Delete(Guid id)
    {
        await _context.Users.Where(s => s.Id == id).ExecuteDeleteAsync();

        await _context.SaveChangesAsync();
        
        return id;
    }

    public async Task<List<User>> GetAll(string filter = "")
    {
        var userEntities= await _context.Users.AsNoTracking().ToListAsync();

        List<User> users = new List<User>();
        
        foreach (var userEntity in userEntities)
        {
            users.Add(User.Create(userEntity.Id, userEntity.UserName, userEntity.PasswordHash, userEntity.Email).user);
        }

        return users;
    }
    
}