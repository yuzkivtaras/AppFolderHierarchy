using DataServices.Data;
using DataServices.IRepository;
using DataServices.IServices;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services
{
    public class CatalogImportService : ICatalogImportService
    {
        private readonly AppDbContext _context;
        private readonly ICatalogRepository _catalogRepository;

        public CatalogImportService(AppDbContext context, ICatalogRepository catalogRepository)
        {
            _context = context;
            _catalogRepository = catalogRepository;
        }

        public async Task ImportCatalog(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        var data = line.Split(',');
                        var catalog = new Catalog
                        {
                            Id = int.Parse(data[0]),
                            NameCatalog = data[1],
                            Level = int.Parse(data[2]),
                        };

                        if (data.Length > 3 && !string.IsNullOrWhiteSpace(data[3]))
                        {
                            int parentId;
                            if (int.TryParse(data[3], out parentId))
                            {
                                catalog.ParentCatalogId = parentId;
                            }
                        }

                        await _catalogRepository.AddCatalogAsync(catalog);
                    }
                }
            }
        }
    }
}
