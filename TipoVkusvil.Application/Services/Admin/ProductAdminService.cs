using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Models;

namespace ClassLibrary1.Services;

public class ProductAdminService : IBaseAdminService<Product>
{
    private readonly IProductRepository _repository;
    
    public ProductAdminService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task Create(Product product)
    {
        await _repository.Create(product);
    }

    public async Task<Guid> Update(Product product)
    {
        return await _repository.Update(product);
    }

    public async Task<Guid> Delete(Guid id)
    {
        return await _repository.Delete(id);
    }

    public async Task<List<Product>> GetAll(string filter = "")
    {
        return await _repository.GetAll();
    }
    
    public async Task<Product> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }
}