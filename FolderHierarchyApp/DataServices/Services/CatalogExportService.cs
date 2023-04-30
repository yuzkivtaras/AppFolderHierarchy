using DataServices.Data;
using DataServices.IServices;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services
{
    public class CatalogExportService : ICatalogExportSevice
    {
        private readonly AppDbContext _context;

        public CatalogExportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MemoryStream> ExportCatalogsAsync()
        {
            var catalogs = await _context.Catalogs.ToListAsync();

            if (catalogs == null || !catalogs.Any())
            {
                return null;
            }

            var sb = new StringBuilder();
            foreach (var catalog in catalogs)
            {
                sb.AppendLine($"{catalog.Id},{catalog.NameCatalog},{catalog.Level},{catalog.ParentCatalogId}");
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            var stream = new MemoryStream(bytes);

            return stream;
        }
    }
}
