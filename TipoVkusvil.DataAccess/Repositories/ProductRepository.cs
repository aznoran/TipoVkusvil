using System.Net;
using System.Security.Cryptography.X509Certificates;
using TipoVkusvil.DataAccess.Entities;
using TipoVkusvil.Models;
using Microsoft.EntityFrameworkCore;
using TipoVkusvil.Core.Abstractions;

namespace TipoVkusvil.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ShopDbContext _shopDbContext;

    public ProductRepository(ShopDbContext shopDbContext)
    {
        _shopDbContext = shopDbContext;
    }

    public async Task Create(Product product)
    {
        var productEntity = new ProductEntity
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            Quantity = product.Quantity,
            Price = product.Price,
            Discount = product.Discount,
            Description = product.Description,
            ShortDescription = product.ShortDescription,
            Mass = product.Mass,
            Kkal = product.Kkal,
            Belki = product.Belki,
            Jiri = product.Jiri,
            Uglevodi = product.Uglevodi,
            ShelfLife = product.ShelfLife,
            CondtionsLife = product.CondtionsLife,
            CompanyName = product.CompanyName,
            ImgURL = product.ImgURL
        };

        await _shopDbContext.Products.AddAsync(productEntity);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task<Guid> Update(Product product)
    {
        await _shopDbContext.Products.Where(pc => pc.ProductId == product.ProductId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.ProductName, b => product.ProductName)
                .SetProperty(b => b.Quantity, b => product.Quantity)
                .SetProperty(b => b.Price, b => product.Price)
                .SetProperty(b => b.Discount, b => product.Discount)
                .SetProperty(b => b.Description, b => product.Description)
                .SetProperty(b => b.ShortDescription, b => product.ShortDescription)
                .SetProperty(b => b.Mass, b => product.Mass)
                .SetProperty(b => b.Kkal, b => product.Kkal)
                .SetProperty(b => b.Belki, b => product.Belki)
                .SetProperty(b => b.Jiri, b => product.Jiri)
                .SetProperty(b => b.Uglevodi, b => product.Uglevodi)
                .SetProperty(b => b.ShelfLife, b => product.ShelfLife)
                .SetProperty(b => b.CondtionsLife, b => product.CondtionsLife)
                .SetProperty(b => b.CompanyName, b => product.CompanyName)
                .SetProperty(b => b.ImgURL, b => product.ImgURL));

        await _shopDbContext.SaveChangesAsync();
        
        return product.ProductId;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await _shopDbContext.Products.Where(s => s.ProductId == id).ExecuteDeleteAsync();
        
        await _shopDbContext.SaveChangesAsync();
        
        return id;
    }

    public async Task<List<Product>> GetAll(string filter = "")
    {
        
        //productEntities = await _shopDbContext.Products.Where(p => p.ProductId == Guid.Parse(filter)).AsNoTracking().ToListAsync();
        
        var productEntities = await _shopDbContext.Products.AsNoTracking().ToListAsync();
        

        var products = productEntities.Select(s => Product.Create(s.ProductId, s.ProductName, s.Quantity, s.Price,
            s.Discount, s.Description, s.ShortDescription, s.Mass, s.Kkal, s.Belki, s.Jiri, s.Uglevodi, s.ShelfLife,
            s.CondtionsLife, s.CompanyName, s.ImgURL).product).ToList();

        return products;
    }

    public async Task<Product> GetById(Guid id)
    {
        return await _shopDbContext.Products.Where(p => p.ProductId == id).Select(s => Product.Create(s.ProductId,
            s.ProductName, s.Quantity, s.Price,
            s.Discount, s.Description, s.ShortDescription, s.Mass, s.Kkal, s.Belki, s.Jiri, s.Uglevodi, s.ShelfLife,
            s.CondtionsLife, s.CompanyName, s.ImgURL).product).FirstOrDefaultAsync() ?? throw new Exception();
        
    }
    
    public async Task<List<Product>> GetByCategoryId(Guid categoryId)
    {
        var a = await _shopDbContext.ProductCategories.Where(s => s.CategoryId == categoryId).Select(p => p.ProductId).ToListAsync();

        var productEntities = await _shopDbContext.Products.AsNoTracking().ToListAsync();

        List<Product> products = new List<Product>();
        
        foreach (var productID in a)
        {
            products.Add(productEntities.Where(p => p.ProductId==productID).Select(s => Product.Create(s.ProductId, s.ProductName, s.Quantity, s.Price,
                s.Discount, s.Description, s.ShortDescription, s.Mass, s.Kkal, s.Belki, s.Jiri, s.Uglevodi, s.ShelfLife,
                s.CondtionsLife, s.CompanyName).product).FirstOrDefault() ?? throw new Exception());
        }

        return products;
    }
    
    /*public async Task<Guid> AddProductToCategory(Guid categoryId, Product product)
    {
        if (product.ProductID == Guid.Empty || categoryId == Guid.Empty)
        {
            //сделать нормальный эксепшн
            throw new Exception();
        }

        var productEntity = new ProductEntity
        {
            ProductID = product.ProductID,
            ProductName = product.ProductName,
            Quantity = product.Quantity,
            Price = product.Price,
            Discount = product.Discount,
            Description = product.Description,
            ShortDescription = product.ShortDescription,
            Mass = product.Mass,
            Kkal = product.Kkal,
            Belki = product.Belki,
            Jiri = product.Jiri,
            Uglevodi = product.Uglevodi,
            ShelfLife = product.ShelfLife,
            CondtionsLife = product.CondtionsLife,
            CompanyName = product.CompanyName,
        };

        var productCategory = new ProductCategory
        {
            ProductId = productEntity.ProductID,
            CategoryId = categoryId,
        };

        await _productDbContext.Products.AddAsync(productEntity);
        await _productDbContext.ProductCategories.AddAsync(productCategory);
        await _productDbContext.SaveChangesAsync();

        return productEntity.ProductID;
    }*/

}