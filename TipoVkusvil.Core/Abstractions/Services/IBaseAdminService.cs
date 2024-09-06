namespace TipoVkusvil.Core.Abstractions;

public interface IBaseAdminService<T>
{
    public Task Create(T category);

    public Task<Guid> Update(T category);

    public Task<Guid> Delete(Guid id);
    
    public Task<List<T>> GetAll(string filter = "");

    public Task<T> GetById(Guid id);
}