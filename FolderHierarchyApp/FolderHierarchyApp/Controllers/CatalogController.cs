using DataServices.IRepository;
using DataServices.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FolderHierarchyApp.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogRepository _repository;
        private readonly ICatalogImportService _importService;
        private readonly ICatalogExportSevice _exportService;

        public CatalogController(ICatalogRepository repository, ICatalogImportService importService, ICatalogExportSevice exportService)
        {
            _repository = repository;
            _importService = importService;
            _exportService = exportService;
        }

        public async Task<IActionResult> Catalog(int? id)
        {
            if (id == null)
            {
                var rootCatalogs = await _repository.GetAllCatalogsAsync();
                return View(rootCatalogs);
            }
            else
            {
                var childCatalogs = await _repository.GetChildCatalogsAsync(id.Value);
                if (childCatalogs == null)
                {
                    return NotFound();
                }
                return View(childCatalogs);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ImportCatalog(IFormFile file)
        {
            await _repository.RemoveCatalogAsync();

            await _importService.ImportCatalog(file);
            return RedirectToAction("Catalog", "Catalog");
        }

        public async Task<IActionResult> ExportCatalog()
        {
            var stream = await _exportService.ExportCatalogsAsync();
            if (stream == null)
            {
                return NotFound();
            }

            return File(stream, "text/plain", "catalogs.txt");
        }
    }
}
