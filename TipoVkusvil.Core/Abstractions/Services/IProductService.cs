using TipoVkusvil.Models;

namespace TipoVkusvil.Core.Abstractions;

public interface IProductService 
{
    public Task<List<Product>> GetAll(string filter = "");
    public Task<Product> GetById(Guid id);
    public Task<List<Product>> GetByCategoryId(Guid categoryId);
}