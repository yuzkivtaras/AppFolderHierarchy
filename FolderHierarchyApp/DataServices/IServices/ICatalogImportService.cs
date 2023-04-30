using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.IServices
{
    public interface ICatalogImportService
    {
        Task ImportCatalog(IFormFile file);
    }
}
