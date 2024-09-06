namespace TipoVkusvil.Core.Abstractions;

public interface IBaseRepository<T>
{
    public Task Create(T category);

    public Task<Guid> Delete(Guid id);
    
    public Task<Guid> Update(T category);
    
    public Task<List<T>> GetAll(string filter = "");

    public Task<T> GetById(Guid id);
    
}