using employeetracking.Models.Employees;
using employeetracking.Service;
using Microsoft.AspNetCore.Mvc;

namespace employeetracking.Controllers
{
    public class PegawaiController : Controller
    {
        private readonly PegawaiService _service;
        private readonly PegawaiImportService _importService;

        public PegawaiController(PegawaiService service, PegawaiImportService importService)
        {
            _service = service;
            _importService = importService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 5, string namaPegawai = "")
        {
            var viewModel = _service.Get(pageNumber, pageSize, namaPegawai);
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Insert()
        {
            ViewData["header"] = "Tabah Data Pegawai";
            var viewModel = _service.Get();
            return View("InsertUpdate", viewModel);
        }
        [HttpPost]
        public IActionResult Insert(PegawaiRequestViewModel insertRequest)
        {
            if (!ModelState.IsValid)
            {
                var vm = _service.Get();
                ViewData["header"] = "Tamabah Data Pegawai";
                return View("InsertUpdate", vm);
            }
            else
            {
                _service.Insert(insertRequest);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("file", "File tidak valid");
                return View();
            }
            var importCounted = await _importService.ImportFileExcel(file);
            TempData["importResult"] = $"{importCounted} berhasil disimpan";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewData["header"] = "Ubah Data Pegawai";
            var model = _service.Get(id);
            return View("InsertUpdate", model);
        }
        [HttpPost]
        public IActionResult Update(int id, PegawaiRequestViewModel updateRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewData["header"] = "Ubah Data Pegawai";
                var vm = _service.Get(id);
                return View("InsertUpdate", vm);
            }
            else
            {
                ViewData["header"] = "Ubah Data Pegawai";
                _service.Update(id, updateRequest);
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                ViewData["message"] = "success";
            }
            catch(Exception ex)
            {
                ViewData["message"] = "failed";
            }
            return RedirectToAction("Index");
        }
    }
}
