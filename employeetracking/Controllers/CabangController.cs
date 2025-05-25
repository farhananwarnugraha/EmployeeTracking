using DocumentFormat.OpenXml.Office2010.Excel;
using employeetracking.Models.Branches;
using employeetracking.Service;
using Microsoft.AspNetCore.Mvc;

namespace employeetracking.Controllers
{
    public class CabangController : Controller
    {
        private readonly CabangService _cabangService;
        private readonly CabangImportService _cabangImportService;

        public CabangController(CabangService cabangService, CabangImportService cabangImportService)
        {
            _cabangService = cabangService;
            _cabangImportService = cabangImportService;
        }

        [HttpGet]
        public IActionResult Index(int pageNumber =1, int pageSize=5, string namaCabang = "")
        {
            var viewModel = _cabangService.Get(pageNumber, pageSize, namaCabang);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("file", "File tidak valid");
                return View();
            }
            var importCounted = await _cabangImportService.ImportFileExcel(file);
            TempData["importResult"] = $"{importCounted} berhasil disimpan";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Insert()
        {
            var viewModel = _cabangService.Get();
            return View("InsertUpdate", viewModel);
        }

        [HttpPost]
        public IActionResult Insert(CabangRequestViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var vm = _cabangService.Get();
                return View("InsertUpdate", vm);
            }
            _cabangService.AddCabang(viewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var model = _cabangService.Get(id);
            return View("InsertUpdate", model);
        }

        [HttpPost]
        public IActionResult Update(int id,CabangRequestViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var vm = _cabangService.Get();
                return View("InsertUpdate", vm);
            }
            _cabangService.UpdateCabang(id, viewModel);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _cabangService.DeleteCabang(id);
                TempData["SuccesMessage"] = "Data Berhasil Dihapus";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Gagal Mengahapus Data " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
