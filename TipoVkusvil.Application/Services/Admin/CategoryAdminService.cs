using TipoVkusvil.Core.Abstractions;
using TipoVkusvil.Models;

namespace ClassLibrary1.Services;

public class CategoryAdminService : IBaseAdminService<Category>
{
    private readonly IBaseRepository<Category> _repository;
    
    public CategoryAdminService(IBaseRepository<Category> repository)
    {
        _repository = repository;
    }


    public async Task Create(Category category)
    {
        await _repository.Create(category);
    }

    public async Task<Guid> Update(Category category)
    {
        return await _repository.Update(category);
    }

    public async Task<Guid> Delete(Guid id)
    {
        return await _repository.Delete(id);
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