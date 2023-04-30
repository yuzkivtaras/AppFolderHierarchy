

using Entities.Models;

namespace DataServices.IRepository
{
    public interface ICatalogRepository
    {
        Task<List<Catalog>> GetAllCatalogsAsync();
        Task<List<Catalog>> GetChildCatalogsAsync(int parentId);
        Task<Catalog> GetCatalogAsync(int id);

        Task RemoveCatalogAsync();
        Task AddCatalogAsync(Catalog catalog);
        Task SaveChangesAsync();
    }
}
