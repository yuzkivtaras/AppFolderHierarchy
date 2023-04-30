using DataServices.Data;
using DataServices.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Repository
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly AppDbContext _context;
        public CatalogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Catalog>> GetAllCatalogsAsync()
        {
            return await _context.Catalogs.Where(c => c.ParentCatalogId == null).ToListAsync();
        }

        public async Task<Catalog> GetCatalogAsync(int id)
        {
            return await _context.Catalogs.FindAsync(id);
        }

        public async Task<List<Catalog>> GetChildCatalogsAsync(int parentId)
        {
            var parentCatalog = await _context.Catalogs.Include(c => c.ChildCatalogs).FirstOrDefaultAsync(c => c.Id == parentId);
            if (parentCatalog == null)
            {
                return null;
            }
            return parentCatalog.ChildCatalogs.ToList();
        }

        public async Task RemoveCatalogAsync()
        {
            _context.Catalogs.RemoveRange(_context.Catalogs);
            await SaveChangesAsync();
        }

        public async Task AddCatalogAsync(Catalog catalog)
        {
            _context.Catalogs.Add(catalog);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
