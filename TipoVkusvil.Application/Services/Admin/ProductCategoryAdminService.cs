using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Models;

namespace ClassLibrary1.Services;

public class ProductCategoryAdminService : IProductCategoryAdminService
{
    private readonly IProductCategoryRepository _repository;
    
    public ProductCategoryAdminService(IProductCategoryRepository repository)
    {
        _repository = repository;
    }

    //Добавить связь
    public async Task Create(ProductCategory productCategory, Product product, Category category)
    {
        await _repository.Create(productCategory, product, category);
    }
    
    //Удалить связь
    public async Task<Guid> Delete(ProductCategory productCategory)
    {
        return await _repository.Delete(productCategory);
    }
    
    /*//Получить все связи 
    public async Task<List<ProductCategory>> GetAll(ProductCategory productCategory)
    {
        return await _repository.GetAll(productCategory);
    }*/
}