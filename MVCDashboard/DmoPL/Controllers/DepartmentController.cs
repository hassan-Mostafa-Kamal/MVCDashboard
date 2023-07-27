using AutoMapper;
using DemoBll.Interfaces;
using DemoBll.Repositories;
using DemoDAL.Models;
using DemoPLL.Interfaces;
using DmoPL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DmoPL.Controllers
{
	[Authorize]

	public class DepartmentController : Controller
	{
		//private readonly /*IDepartmentRepository*/ departmentRepository;
		private readonly IUniteOfWork UniteOfWork;
		private readonly IMapper mapper;

		public DepartmentController(IUniteOfWork _UniteOfWork, IMapper _mapper)
		{
			//departmentRepository=_departmentRepository;
			UniteOfWork = _UniteOfWork;
			mapper = _mapper;
		}
		public async Task<IActionResult> Index()
		{
			var departments= await UniteOfWork.DepartmentRepository.GetAll();
			var departmentsVm = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);

			return View(departmentsVm);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(DepartmentViewModel deptVM)
		{
			if(ModelState.IsValid)
			{

				//if (Cnt == 0)
				//	TempData["Message"] = "There Is No Depatrment";
				/*int Cnt=*/
				var dept = mapper.Map<DepartmentViewModel, Department>(deptVM);

				UniteOfWork.DepartmentRepository.Add(dept);
				await UniteOfWork.Complete();
				return RedirectToAction(nameof(Index));
			}
			return View(deptVM);
		}
		public async Task<IActionResult> Details(int? id,string name="Details")
		{
			if (id is  null)
				return BadRequest();
			
				var depart =await UniteOfWork.DepartmentRepository.Get(id.Value);
			if (depart == null) return NotFound();
			var departVm = mapper.Map<Department, DepartmentViewModel>(depart);

			return View(name, departVm);
			
		}
		public async Task<IActionResult> Edit(int? id)
		{
			//if (id is null)
			//	return BadRequest();

			//var depart = departmentRepository.Get(id.Value);
			//if (depart == null) return NotFound();
			//return View(depart);
			return await Details(id,"Edit");
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]

		public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel deptVM)
		{
			if (id != deptVM.Id) return BadRequest();

			if (ModelState.IsValid)
			{
				try
				{
					var dept = mapper.Map<DepartmentViewModel, Department>(deptVM);
					UniteOfWork.DepartmentRepository.Update(dept);
					await UniteOfWork.Complete();
					return RedirectToAction(nameof(Index));
				}
				catch (System.Exception ex)
				{

					ModelState.AddModelError(string.Empty, ex.Message);	
				}
				
			}
			return View(deptVM);
		}
		public async Task<IActionResult> Delete(int? id) // action for Update department
		{

			return await Details(id, "Delete");

		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]

		public async Task<IActionResult> Delete([FromRoute] int id, DepartmentViewModel deptVM) // action for Update department
		{
			if (id != deptVM.Id) return BadRequest();
			try
			{
				var dept = mapper.Map<DepartmentViewModel, Department>(deptVM);
				UniteOfWork.DepartmentRepository.Delete(dept);
				await UniteOfWork.Complete();
				return RedirectToAction(nameof(Index));

			}
			catch (Exception Ex)
			{
				ModelState.AddModelError(string.Empty, Ex.Message);
			}
			return View(deptVM);


		}
	}
}
