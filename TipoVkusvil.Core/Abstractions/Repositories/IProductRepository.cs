using TipoVkusvil.Models;

namespace TipoVkusvil.Core.Abstractions;

public interface IProductRepository 
{
    public Task Create(Product category);

    public Task<Guid> Delete(Guid id);
    
    public Task<Guid> Update(Product category);
    
    public Task<List<Product>> GetAll(string filter = "");

    public Task<Product> GetById(Guid id);
    public Task<List<Product>> GetByCategoryId(Guid categoryId);
}