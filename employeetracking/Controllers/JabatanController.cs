using employeetracking.Models.Positions;
using employeetracking.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace employeetracking.Controllers
{
    public class JabatanController : Controller
    {
        private readonly JabatanService _service;
        private readonly JabtanInportService _inportService;

        public JabatanController(JabatanService service, JabtanInportService inportService)
        {
            _service = service;
            _inportService = inportService;
        }

        [HttpGet("jabatan")]
        public IActionResult Index(int pageNumber = 1, int pageSize =5, string namaJabatan = "")
        {
            var viewModel = _service.Get(pageNumber, pageSize, namaJabatan);
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                ModelState.AddModelError("file", "File tidak valid");
                return View();
            }
            var importCounted = await _inportService.ImportFileExcel(file);
            TempData["importResult"] = $"{importCounted} berhasil disimpan";
            return RedirectToAction("Index");
        }

        [HttpGet("jabatan/insert")]
        public IActionResult Insert()
        {
            var ViewModel = _service.Get();
            return View("InsertUpdate", ViewModel);
        }

        [HttpPost("jabatan/insert")]
        public IActionResult Insert(JabatanRequestViewModel vieeModel)
        {
            if (!ModelState.IsValid)
            {
                var vm = _service.Get();
                return View("InsertUpdate", vm);
            }

            _service.AddJabatan(vieeModel);
            return RedirectToAction("Index");
        }

        [HttpGet("jabatan/update/{jabtanId}")]
        public IActionResult Update(int jabtanId)
        {
            var vm = _service.Get(jabtanId);
            return View("InsertUpdate", vm);
        }

        [HttpPost("jabatan/update/{jabtanId}")]
        public IActionResult Update(int jabtanId, JabatanRequestViewModel vieModelUpdate)
        {
            if (!ModelState.IsValid)
            {
                var vm = _service.Get();
                return View("InsertUpdate", vm);
            }

            _service.UpdateJabatan(jabtanId, vieModelUpdate);
            ViewData["message"] = "Update Success";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                TempData["SuccesMessage"] = "Data Berhasil Dihapus";
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "Gagal Mengahapus Data " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
