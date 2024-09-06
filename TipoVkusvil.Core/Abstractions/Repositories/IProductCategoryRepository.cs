using TipoVkusvil.Models;

namespace TipoVkusvil.Core.Abstractions;

public interface IProductCategoryRepository
{
    
    //Добавить связь
    public Task Create(ProductCategory productCategory, Product product, Category category);
    
    //Удалить связь
    public Task<Guid> Delete(ProductCategory productCategory);
    
    //Получить все связи (или только определенные)
   // public Task<List<ProductCategory>> GetAll(ProductCategory productCategory);
}