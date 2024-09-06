using Microsoft.EntityFrameworkCore;
using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.DataAccess.Entities;
using TipoVkusvil.Models;

namespace TipoVkusvil.DataAccess.Repositories;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly ShopDbContext _shopDbContext;

    public ProductCategoryRepository(ShopDbContext shopDbContext)
    {
        _shopDbContext = shopDbContext;
    }

    //Добавить связь
    public async Task Create(ProductCategory productCategory, Product product, Category category)
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
        
        var categoryEntity = new CategoryEntity
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            UpperCategoryName = category.UpperCategoryName,
            ImgURL = category.ImgURL
        };

        ProductCategoryEntity productCategoryEntity = new ProductCategoryEntity()
        {
            Category = categoryEntity,
            CategoryId = productCategory.CategoryId,
            Product = productEntity,
            ProductId = productCategory.ProductId
        };
            
        await _shopDbContext.ProductCategories.AddAsync(productCategoryEntity);
        await _shopDbContext.SaveChangesAsync();
    }
    
    //Удалить связь
    public async Task<Guid> Delete(ProductCategory productCategory)
    {
        
        //Удалить всю категорию с определенным id и связи с продуктами
        if (productCategory.ProductId == Guid.Empty)
        {
            await _shopDbContext.ProductCategories.Where(s => s.CategoryId == productCategory.CategoryId).ExecuteDeleteAsync();
        }
        //Удалить все продукты с определенным id и связи с категориями
        else if (productCategory.CategoryId == Guid.Empty)
        {
            await _shopDbContext.ProductCategories.Where(s => s.ProductId == productCategory.ProductId).ExecuteDeleteAsync();
        }
        //Удалить определенную связь
        else
        {
            await _shopDbContext.ProductCategories.Where(s => s.ProductId == productCategory.ProductId).Where(s => s.CategoryId==productCategory.CategoryId).ExecuteDeleteAsync();
        }
        await _shopDbContext.SaveChangesAsync();
        return productCategory.CategoryId;
    }
    
    /*//Получить все связи 
    public async Task<List<ProductCategory>> GetAll(ProductCategory productCategory)
    {
        List<ProductCategory> productCategories;

        //Вернуть все связи
        if (productCategory.ProductId == Guid.Empty && productCategory.CategoryId == Guid.Empty)
        {
            productCategories = await _shopDbContext.ProductCategories.AsNoTracking().ToListAsync();
        }
        //Получить все связи для определенной категории
        else if (productCategory.ProductId == Guid.Empty)
        {
            productCategories = await _shopDbContext.ProductCategories.Where(s => s.CategoryId==productCategory.CategoryId).AsNoTracking().ToListAsync();
        }
        //Получить все связи для определенного продукта
        else if (productCategory.CategoryId == Guid.Empty)
        {
            productCategories = await _shopDbContext.ProductCategories.Where(s => s.ProductId==productCategory.ProductId).AsNoTracking().ToListAsync();
        }
        //Вернуть определенную связь
        else
        {
            productCategories = await _shopDbContext.ProductCategories.Where(s => s.ProductId==productCategory.ProductId).Where(s => s.CategoryId==productCategory.CategoryId).AsNoTracking().ToListAsync();
        }
        
        return productCategories;
    }*/
}