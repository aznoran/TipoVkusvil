namespace TipoVkusvil.Core.Abstractions;

public interface IBaseService<T>
{
    public Task<List<T>> GetAll(string filter = "");
    public Task<T> GetById(Guid id);
}