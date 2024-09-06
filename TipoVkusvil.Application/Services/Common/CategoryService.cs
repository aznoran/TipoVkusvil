using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Models;

namespace ClassLibrary1.Services;

public class CategoryService : IBaseService<Category>
{
    private readonly IBaseRepository<Category> _repository;
    
    public CategoryService(IBaseRepository<Category> repository)
    {
        _repository = repository;
    }

    public async Task<List<Category>> GetAll(string filter = "")
    {
        return await _repository.GetAll();
    }
    
    public async Task<Category> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }
}