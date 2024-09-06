using Microsoft.EntityFrameworkCore;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.DataAccess.Entities;
using TipoVkusvil.Models;

namespace TipoVkusvil.DataAccess.Repositories;

public class CategoryRepository : IBaseRepository<Category>
{
    private readonly ShopDbContext _shopDbContext;

    public CategoryRepository(ShopDbContext shopDbContext)
    {
        _shopDbContext = shopDbContext;
    }

    public async Task Create(Category category)
    {
        var categoryEntity = new CategoryEntity
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            UpperCategoryName = category.UpperCategoryName,
            ImgURL = category.ImgURL
        };

        await _shopDbContext.Categories.AddAsync(categoryEntity);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task<Guid> Update(Category category)
    {
        await _shopDbContext.Categories.Where(pc => pc.CategoryId == category.CategoryId)
            .ExecuteUpdateAsync(s => s.SetProperty(s => s.CategoryName, category.CategoryName));

        await _shopDbContext.SaveChangesAsync();
        
        return category.CategoryId;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await _shopDbContext.Categories.Where(s => s.CategoryId == id).ExecuteDeleteAsync();

        await _shopDbContext.SaveChangesAsync();
        
        return id;
    }
    
    public async Task<List<Category>> GetAll(string filter = "")
    {
        /*if (filter != "")
        {
            сделать фильтр
        }*/

        var categoryEntities = await _shopDbContext.Categories.AsNoTracking().ToListAsync();

        var categories = categoryEntities.Select(s => Category.Create(s.CategoryId, s.CategoryName, s.UpperCategoryName, s.ImgURL).category).ToList();

        return categories;
    }
    
    public async Task<Category> GetById(Guid id)
    {
        return await _shopDbContext.Categories.Where(p => p.CategoryId == id).Select(s => Category.Create(s.CategoryId, s.CategoryName, s.UpperCategoryName, s.ImgURL).category).FirstOrDefaultAsync();
        
    }

    
}