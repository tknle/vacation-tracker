using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
//using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveRequestController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<Employee> userManager
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        // GET: LeaveRequestController
        public async Task<ActionResult> Index()
        {
            var leaveRequests = await _unitOfWork.LeaveRequests.FindAll(
                includes: new List<string> { "RequestingEmployee", "LeaveType" });
            var leaveRequestModel = _mapper.Map<List<LeaveRequestVM>>(leaveRequests);
            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests = leaveRequestModel.Count,
                ApprovedRequests = leaveRequestModel.Count(q => q.Approved == true),
                PendingRequests = leaveRequestModel.Count(q => q.Approved == null),
                RejectedRequests = leaveRequestModel.Count(q => q.Approved == false),
                LeaveRequests = leaveRequestModel
            };
            return View(model);
        }

        // GET: LeaveRequestController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var leaveRequest = await _unitOfWork.LeaveRequests.Find(q => q.Id == id,
               includes: new List<string> { "ApprovedBy", "RequestingEmployee", "LeaveType" });
            var model = _mapper.Map<LeaveRequestVM>(leaveRequest);
            return View(model);
        }

        public async Task<ActionResult> ApproveRequest(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var leaveRequest = await _unitOfWork.LeaveRequests.Find(q => q.Id == id);
                var employeeId = leaveRequest.RequestingEmployeeId;
                var leaveTypeId = leaveRequest.LeaveTypeId;
                var period = DateTime.Now.Year;

                var allocation = await _unitOfWork.LeaveAllocations.Find(q => q.EmployeeId == employeeId
                                                    && q.Period == period
                                                    && q.LeaveTypeId == leaveTypeId);

                double daysRequested = GetBusinessDays(leaveRequest.StartDate, leaveRequest.EndDate);

                if (leaveRequest.HalfDayOff == true)
                //{
                //    if (IsHoliDay(leaveRequest.StartDate, leaveRequest.EndDate).Result == true)
                //    {
                //        //If get helf day off on holiday
                        daysRequested -= GetHalfDay(leaveRequest.HalfDayOff).Result;
                //    }
                //    else
                //    {
                //        daysRequested -= GetHalfDay(leaveRequest.HalfDayOff).Result; //halfdayOff
                //    }
                //}
                //else
                //{
                //    if (IsHoliDay(leaveRequest.StartDate, leaveRequest.EndDate).Result == true)
                //    {
                //        daysRequested -= GetHolidays(leaveRequest.StartDate, leaveRequest.EndDate).Result; // a day off
                //    }
                //}

                allocation.NumberOfDays = (int)(allocation.NumberOfDays - daysRequested);

                leaveRequest.Approved = true;
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateActioned = DateTime.Now;

                _unitOfWork.LeaveRequests.Update(leaveRequest);
                _unitOfWork.LeaveAllocations.Update(allocation);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public async Task<ActionResult> RejectRequest(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var leaveRequest = await _unitOfWork.LeaveRequests.Find(q => q.Id == id);
                leaveRequest.Approved = false;
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateActioned = DateTime.Now;

                _unitOfWork.LeaveRequests.Update(leaveRequest);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        // GET: LeaveRequestController/Create
        public async Task<ActionResult> Create()
        {
            var leaveTypes = await _unitOfWork.LeaveTypes.FindAll();

            var leaveTypeItems = leaveTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });
            var model = new CreateLeaveRequestVM
            {
                LeaveTypes = leaveTypeItems
            };
            return View(model);
        }

        // POST: LeaveRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveRequestVM model)
        {
            try
            {
                var startDate = Convert.ToDateTime(model.StartDate);
                var endDate = Convert.ToDateTime(model.EndDate);
                var leaveTypes = await _unitOfWork.LeaveTypes.FindAll();
                var employee = await _userManager.GetUserAsync(User);
                var period = DateTime.Now.Year;
                var allocation = await _unitOfWork.LeaveAllocations.Find(q => q.EmployeeId == employee.Id
                                                   && q.Period == period
                                                   && q.LeaveTypeId == model.LeaveTypeId);

                double daysRequested = GetBusinessDays(startDate, endDate);

                if (model.HalfDayOff == true)
                //{
                //    if (IsHoliDay(startDate, endDate).Result == true)
                //    {
                //        //If get helf day off on holiday
                //        daysRequested -= GetHalfDay(model.HalfDayOff).Result;
                //    }
                //    else
                //    {
                        daysRequested -= GetHalfDay(model.HalfDayOff).Result; //halfdayOff
                //    }
                //}
                //else
                //{
                //    if (IsHoliDay(startDate, endDate).Result == true)
                //    {
                //        daysRequested -= GetHolidays(startDate, endDate).Result; // a day off
                //    }
                //}


                var leaveTypeItems = leaveTypes.Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Id.ToString()
                });
                model.LeaveTypes = leaveTypeItems;

                if (allocation == null)
                {
                    ModelState.AddModelError("", "You Have No Days Left");
                }
                if (DateTime.Compare(startDate, endDate) > 1)
                {
                    ModelState.AddModelError("", "Start Date cannot be earlier than or the same as the End Date");
                    return View(model);
                }
                if (daysRequested > allocation.NumberOfDays)
                {
                    ModelState.AddModelError("", "You Do Not Sufficient Days For This Request");
                }
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leaveRequestModel = new LeaveRequestVM
                {
                    RequestingEmployeeId = employee.Id,
                    StartDate = startDate,
                    EndDate = endDate,
                    Approved = null,
                    DateRequested = DateTime.Now,
                    DateActioned = DateTime.Now,
                    LeaveTypeId = model.LeaveTypeId,
                    RequestComments = model.RequestComments,
                    TotalDays = daysRequested,
                    HalfDayOff = model.HalfDayOff
                };

                var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestModel);
                await _unitOfWork.LeaveRequests.Create(leaveRequest);
                await _unitOfWork.Save();

                return RedirectToAction("MyLeave");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
        }

        public async Task<ActionResult> RevertRequest(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var leaveRequest = await _unitOfWork.LeaveRequests.Find(q => q.Id == id);
                var employeeId = leaveRequest.RequestingEmployeeId;
                var leaveTypeId = leaveRequest.LeaveTypeId;
                var period = DateTime.Now.Year;

                var allocation = await _unitOfWork.LeaveAllocations.Find(q => q.EmployeeId == employeeId
                                                    && q.Period == period
                                                    && q.LeaveTypeId == leaveTypeId);

                double daysRequested = GetBusinessDays(leaveRequest.StartDate, leaveRequest.EndDate);


                if (leaveRequest.HalfDayOff == true)
                //{
                //    if (IsHoliDay(leaveRequest.StartDate, leaveRequest.EndDate).Result == true)
                //    {
                //        //If get helf day off on holiday
                        daysRequested -= GetHalfDay(leaveRequest.HalfDayOff).Result;
                //    }
                //    else
                //    {
                //        daysRequested -= GetHalfDay(leaveRequest.HalfDayOff).Result; //halfdayOff
                //    }
                //}
                //else
                //{
                //    if (IsHoliDay(leaveRequest.StartDate, leaveRequest.EndDate).Result == true)
                //    {
                //        daysRequested -= GetHolidays(leaveRequest.StartDate, leaveRequest.EndDate).Result; // a day off
                //    }
                //}

                allocation.NumberOfDays = (int)(allocation.NumberOfDays + daysRequested);

                leaveRequest.Approved = false;
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateActioned = DateTime.Now;

                _unitOfWork.LeaveRequests.Update(leaveRequest);
                _unitOfWork.LeaveAllocations.Update(allocation);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        //Test import list of holidays with Excel
        //public async Task<List<HolidaysVM>> ImportHolidays(IFormFile file)
        //{
        //    var list = new List<HolidaysVM>();
        //    using (var stream = new MemoryStream())
        //    {
        //        await file.CopyToAsync(stream);
        //        using var package = new ExcelPackage(stream);
        //        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
        //        var rowcount = worksheet.Dimension.Rows;
        //        for (int row = 2; row < rowcount; row++)
        //        {
        //            list.Add(new HolidaysVM
        //            {
        //                Id = worksheet.Cells[row, 1].Value.ToString().Trim(),
        //                HolidayName = worksheet.Cells[row, 2].Value.ToString().Trim(),
        //                Date = worksheet.Cells[row, 3].Value.ToString().Trim()
        //            });
        //        }
        //    }
        //    return list;
        //}
        //Return a list of Holidays
        //private static List<DateTime> GetHolidays(DateTime holiday)
        //{
        //    List<DateTime> holidays = new List<DateTime>();

        //    DateTime holidayDate = AdjustForWeekendHoliday(new DateTime(holiday.Year, holiday.Month, holiday.Day).Date);
        //    holidays.Add(holidayDate);

        //    return holidays;
        //}

        //Calculate business days
        public static double GetBusinessDays(DateTime startD, DateTime endD)
        {
            double calcBusinessDays =
                1 + ((endD - startD).TotalDays * 5 -
                (startD.DayOfWeek - endD.DayOfWeek) * 2) / 7;

            if (endD.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
            if (startD.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

            return calcBusinessDays;
        }

        //public async Task<bool> IsHoliDay(DateTime startD, DateTime endD)
        //{
        //    var dateToCheck = startD;
        //    var holidaytypes = await _unitOfWork.Holidays.FindAll();
        //    var list = _mapper.Map<List<Holiday>, List<HolidayVM>>(holidaytypes.ToList());
        //    bool isHoliday = false;
        //    while (dateToCheck >= startD && dateToCheck < endD)
        //    {
        //        foreach (var holiday in list)
        //        {
        //            DateTime dateTime = DateTime.Parse(holiday.Date);
        //            if (dateToCheck == dateTime)
        //            {
        //                isHoliday = true;
        //            }
        //        }
        //        dateToCheck = dateToCheck.AddDays(1);
        //    }
        //    return isHoliday;
        //}

        //public async Task<double> GetHolidays(DateTime startD, DateTime endD)
        //{
        //    var dateToCheck = startD;
        //    var holidaytypes = await _unitOfWork.Holidays.FindAll();
        //    var list = _mapper.Map<List<Holiday>, List<HolidayVM>>(holidaytypes.ToList());

        //    double noOfDays = 0.0;

        //    while (dateToCheck >= startD && dateToCheck < endD)
        //    {
        //        foreach (var holiday in list)
        //        {
        //            DateTime dateTime = DateTime.Parse(holiday.Date);
        //            if (dateToCheck == dateTime)
        //            {
        //                noOfDays += 1.0;
        //            }
        //        }
        //        dateToCheck = dateToCheck.AddDays(1);
        //    }
        //    return noOfDays;
        //}

        public async Task<double> GetHalfDay(bool halfDayOff)
        {
            double dayOff = 0.0;
            // var leaveRequest = await _unitOfWork.LeaveRequests.Find(q => q.Id == id);
            if (halfDayOff == true)
            {
                dayOff = 0.5;
            }
            return dayOff;
        }

        // GET: LeaveRequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //employee cancle a request
        public async Task<ActionResult> CancelRequest(int id)
        {
            var leaveRequest = await _unitOfWork.LeaveRequests.Find(q => q.Id == id);
            leaveRequest.Cancelled = true;
            _unitOfWork.LeaveRequests.Update(leaveRequest);
            await _unitOfWork.Save();
            return RedirectToAction("MyLeave");
        }

        //My leave action for employee view
        public async Task<ActionResult> MyLeave()
        {
            var employee = await _userManager.GetUserAsync(User);
            var employeeId = employee.Id;
            var employeeAllocations = await _unitOfWork.LeaveAllocations.FindAll(q => q.EmployeeId == employeeId,
                includes: new List<string> { "LeaveType" });

            var employeeRequests = await _unitOfWork.LeaveRequests
                .FindAll(q => q.RequestingEmployeeId == employeeId);

            var employeeAllocationsModel = _mapper.Map<List<LeaveAllocationVM>>(employeeAllocations);
            var employeeRequestModel = _mapper.Map<List<LeaveRequestVM>>(employeeRequests);

            var model = new EmployeeLeaveRequestViewVM
            {
                LeaveAllocations = employeeAllocationsModel,
                LeaveRequests = employeeRequestModel
            };
            return View(model);
        }



        // GET: LeaveRequestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}