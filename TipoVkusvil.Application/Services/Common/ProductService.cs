using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Models;

namespace ClassLibrary1.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    
    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> GetAll(string filter = "")
    {
        return await _repository.GetAll();
    }
    
    public async Task<List<Product>> GetByCategoryId(Guid categoryId)
    {
        return await _repository.GetByCategoryId(categoryId);
    }
    
    public async Task<Product> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }
}