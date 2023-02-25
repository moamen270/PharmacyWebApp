using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using PharmacyWebApp.Models.ViewModels;
using System.Diagnostics;

namespace PharmacyWebApp.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public PrescriptionController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            string docId = (await _userManager.GetUserAsync(User)).Id;
            IEnumerable<Prescription> obj = await _unitOfWork.Prescription.GetAllAsync(new string[] { "Patient" });
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> QuickCreate()
        {
            await _unitOfWork.Prescription.AddAsync(new Prescription
            {
                DoctorId = (await _userManager.GetUserAsync(User)).Id,
                PatientId = (await _userManager.GetUserAsync(User)).Id,
            });
            _unitOfWork.Save();
            return Json(new { success = true, message = "Product Created Successfully" });
        }

        //POST

        public async Task<IActionResult> Create()
        {
            var docId = (await _userManager.GetUserAsync(User)).Id;
            var vm = new PrescriptionVM { DoctorId = docId };
            return View("PrescriptionForm", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PrescriptionVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            if (await _userManager.FindByIdAsync(viewModel.PatientId) == null)
            {
                return View(viewModel);
            }
            viewModel.DoctorId = (await _userManager.GetUserAsync(User)).Id;
            await _unitOfWork.Prescription.AddAsync(_mapper.Map<Prescription>(viewModel));
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var obj = await _unitOfWork.Prescription.GetFirstOrDefaultAsync(item => item.Id == id);
            if (obj == null)
                return Json(new { success = false, message = "Error While Deleting" });

            _unitOfWork.Prescription.Delete(obj);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Prescription Deleted Successfully" });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var obj = await _unitOfWork.Prescription.GetFirstOrDefaultAsync(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<PrescriptionVM>(obj));
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PrescriptionVM obj)
        {
            if (ModelState.IsValid)
            {
                return View(obj);
            }

            _unitOfWork.Prescription.Update(_mapper.Map<Prescription>(obj));
            _unitOfWork.Save();
            TempData["success"] = "Prescription updated successfully";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}