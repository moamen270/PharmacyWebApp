using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using PharmacyWebApp.Models.ViewModels;
using System.Diagnostics;

namespace PharmacyWebApp.Controllers
{
    public class PrescriptionDetailsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public PrescriptionDetailsController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int PID)
        {
            IEnumerable<PrescriptionDetails> obj = await _unitOfWork.PrescriptionDetails.GetAllByFilterAsync(f => f.PrescriptionId == PID);
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> QuickCreate(int PID)
        {
            await _unitOfWork.PrescriptionDetails.AddAsync(new PrescriptionDetails
            {
                PrescriptionId = PID
            });
            _unitOfWork.Save();
            return Json(new { success = true, message = "Product Created Successfully" });
        }

        //POST

        public async Task<IActionResult> Create(int PID)
        {
            var vm = new PrescriptionDetailsVM { PrescriptionId = PID };
            return View("PrescriptionDetailsForm", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PrescriptionDetailsVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _unitOfWork.PrescriptionDetails.AddAsync(_mapper.Map<PrescriptionDetails>(viewModel));
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var obj = await _unitOfWork.PrescriptionDetails.GetFirstOrDefaultAsync(item => item.Id == id);
            if (obj == null)
                return Json(new { success = false, message = "Error While Deleting" });

            _unitOfWork.PrescriptionDetails.Delete(obj);
            _unitOfWork.Save();

            return Json(new { success = true, message = "PrescriptionDetails Deleted Successfully" });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var obj = await _unitOfWork.PrescriptionDetails.GetFirstOrDefaultAsync(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<PrescriptionDetailsVM>(obj));
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PrescriptionDetailsVM obj)
        {
            if (ModelState.IsValid)
            {
                return View(obj);
            }

            _unitOfWork.PrescriptionDetails.Update(_mapper.Map<PrescriptionDetails>(obj));
            _unitOfWork.Save();
            TempData["success"] = "PrescriptionDetails updated successfully";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}