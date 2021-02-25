using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class HolidayController : Controller
    {
        private readonly IHolidayRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HolidayController(IHolidayRepository repo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // GET: HolidayController
        public async Task<ActionResult> Index()
        {
            var holidays = await _unitOfWork.Holidays.FindAll();
            var model = _mapper.Map<List<Holiday>, List<HolidayVM>>(holidays.ToList());

            return View(model);
        }

        // GET: HolidayController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //var isExist = await _repo.isExists(id);
            var isExists = await _unitOfWork.Holidays.isExists(q => q.Id == id);
            if (!isExists)
            {
                return NotFound();
            }
            //var leavetype = await _repo.FindById(id);
            var leavetype = await _unitOfWork.Holidays.Find(q => q.Id == id);
            var model = _mapper.Map<HolidayVM>(leavetype);
            return View(model);
        }

        // GET: HolidayController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HolidayController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HolidayVM model)
        {
            try
            {             
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                
                var holiday = _mapper.Map<Holiday>(model);

                holiday.DateCreated = DateTime.Now;

                await _unitOfWork.Holidays.Create(holiday);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went Wrong...");
                return View();
            }
        }

        // GET: HolidayController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var isExists = await _unitOfWork.Holidays.isExists(q => q.Id == id);
            if (!isExists)
            {
                return NotFound();
            }
            //var leavetype = await _repo.FindById(id);
            var holiday = await _unitOfWork.Holidays.Find(q => q.Id == id);
            var model = _mapper.Map<HolidayVM>(holiday);
            return View(model);
        }


        // POST: HolidayController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public ActionResult Edit(int id, IFormCollection collection)
        public async Task<ActionResult> Edit(HolidayVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var holiday = _mapper.Map<Holiday>(model);
                
                _unitOfWork.Holidays.Update(holiday);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went Wrong...");
                return View(model);
            }
        }

        // GET: HolidayController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            //var leavetype = await _repo.FindById(id);
            var holiday = await _unitOfWork.Holidays.Find(expression: q => q.Id == id);
            //if nothing is return
            if (holiday == null)
            {
                return NotFound();
            }
            _unitOfWork.Holidays.Delete(holiday);
            await _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // POST: HolidayController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, HolidayVM model)
        {
            try
            {
                var holiday = await _unitOfWork.Holidays.Find(expression: q => q.Id == id);
                if (holiday == null)
                {
                    return NotFound();
                }
                _unitOfWork.Holidays.Delete(holiday);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
