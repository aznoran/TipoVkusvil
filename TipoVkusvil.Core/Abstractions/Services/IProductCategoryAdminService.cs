using TipoVkusvil.Models;

namespace TipoVkusvil.Core.Abstractions;

public interface IProductCategoryAdminService
{
    public Task Create(ProductCategory productCategory, Product product, Category category);

    public Task<Guid> Delete(ProductCategory productCategory);
}