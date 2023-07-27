using AutoMapper;
using DemoBll.Interfaces;
using DemoBll.Repositories;
using DemoDAL.Models;
using DemoPLL.Interfaces;
using DmoPL.Helper;
using DmoPL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DemoPl.Controllers
{
	[Authorize]

	public class EmployeeController : Controller
	{

		//private readonly IEmployeeRepository EmpRepository;
		//private readonly IDepartmentRepository departmentRepository;
		private readonly IUniteOfWork UnitOfWork;
		private readonly IMapper mapper;

		public EmployeeController(IUniteOfWork _UnitOfWork,
			IMapper _mapper) //Dependency injection
		{
			UnitOfWork = _UnitOfWork;
			//EmpRepository = _EmpRepository;
			//departmentRepository = _DepartmentRepository;
			mapper = _mapper;
		}
		public async Task<IActionResult> Index(string SearchValue) // action for main page of Employee
		{
			//ViewBag.Messag = "Hello View Bag";
			//ViewData["Message"] = "Hello View Data";

			//Employees.Count();
			IEnumerable<Empployee> Employees;
			if (string.IsNullOrEmpty(SearchValue))
			{
				 Employees = await UnitOfWork.EmployeeRepository.GetAll(); 
			}
			else {
				 Employees = UnitOfWork.EmployeeRepository.SearchByName(SearchValue);
			}
			var EmployeeVm = mapper.Map<IEnumerable<Empployee>, IEnumerable<EmployeeViewModel>>(Employees);
			return View(EmployeeVm);

		}


		[HttpGet]
		public async Task<IActionResult> Create() // action for Create Employee
		{
			ViewData["Departments"] = await UnitOfWork.DepartmentRepository.GetAll();
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(EmployeeViewModel EmployeeVM) // action for Create Employee
		{
			if (ModelState.IsValid)
			{
				//var Employee=new Empployee() { Address= EmployeeVM.Address,Age= EmployeeVM.Age
				//,Name= EmployeeVM.Name,Email= EmployeeVM.Email,Salary= EmployeeVM.Salary,Phone= EmployeeVM.Phone,
				//DepartmentId= EmployeeVM.DepartmentId,IsActive= EmployeeVM.IsActive,HireDate= EmployeeVM.HireDate
				//};
				/*int Cnt=*/
				//if (Cnt > 0)
				//	TempData["Emp"] = $"{Cnt} Employee Founded";
				EmployeeVM.ImageName = DocumentSettings.Upload(EmployeeVM.Image, "images");
				var Employee = mapper.Map<EmployeeViewModel, Empployee>(EmployeeVM);
				await UnitOfWork.EmployeeRepository.Add(Employee);
				await UnitOfWork.Complete();
				return RedirectToAction(nameof(Index));
			}
			return View(EmployeeVM);
		}
		public async Task<IActionResult> Details(int? id, string ViewName = "Details") // action for Details of Employee
		{
			if (id is null)
			{
				return BadRequest();
			}
			var Employee = await UnitOfWork.EmployeeRepository.Get(id.Value);
			if (Employee is null)
				return NotFound();
			var EmployeeVm = mapper.Map<Empployee, EmployeeViewModel>(Employee);
			///////////////////////////////////////////////////////////////////////////////////////////
			if (Employee.DepartmentId.HasValue)
				ViewData["deptname"] = (await UnitOfWork.DepartmentRepository.Get(Employee.DepartmentId.Value)).Name;
			else ViewData["deptname"] = "NA";
			///////////////////////////////////////////////////////////////////////////////////////////
			return View(ViewName, EmployeeVm);
		}
		public async Task<IActionResult> Edit(int? id) // action for Update Employee
		{
			//if (id is null)
			//{
			//	return BadRequest();
			//}
			//var Employee = EmpRepository.Get(id.Value);
			//if (Employee is null)
			//	return NotFound();

			//return View(Employee);
			ViewData["Departments"] = await UnitOfWork.DepartmentRepository.GetAll();

			return await Details(id, "Edit");

		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel EmployeeVM) // action for Update Employee using post method
		{
			if (id != EmployeeVM.Id) return BadRequest();
			if (ModelState.IsValid)
			{
				try
				{
					EmployeeVM.ImageName = DocumentSettings.Upload(EmployeeVM.Image, "images");
					var Employee = mapper.Map<EmployeeViewModel, Empployee>(EmployeeVM);
					UnitOfWork.EmployeeRepository.Update(Employee);
				await UnitOfWork.Complete();
					return RedirectToAction(nameof(Index));

				}
				catch (Exception Ex)
				{
					ModelState.AddModelError(string.Empty, Ex.Message);
				}
			}
			return View(EmployeeVM);

		}
		public async Task<IActionResult> Delete(int? id) // action for Update Employee
		{

			return await Details(id, "Delete");

		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]

		public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel EmployeeVM) // action for Update Employee
		{
			if (id != EmployeeVM.Id) return BadRequest();
			try
			{
				var Employee=mapper.Map<EmployeeViewModel,Empployee>(EmployeeVM);
				UnitOfWork.EmployeeRepository.Delete(Employee);
				int Cnt=await UnitOfWork.Complete();
				if (Cnt > 0)
					DocumentSettings.Delete(EmployeeVM.ImageName, "Images");
				return RedirectToAction(nameof(Index));

			}
			catch (Exception Ex)
			{
				ModelState.AddModelError(string.Empty, Ex.Message);
				return View(EmployeeVM);
			}


		}
	}
}

