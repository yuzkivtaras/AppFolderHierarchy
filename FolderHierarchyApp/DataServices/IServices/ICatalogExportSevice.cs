using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.IServices
{
    public interface ICatalogExportSevice
    {
        Task<MemoryStream> ExportCatalogsAsync();
    }
}
