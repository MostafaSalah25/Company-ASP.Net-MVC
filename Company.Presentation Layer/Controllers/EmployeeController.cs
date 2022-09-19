using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Entities;
using Company.Presentation_Layer.Helper;
using Company.Presentation_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Presentation_Layer.Controllers
{
    [Authorize] 
    public class EmployeeController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }

        public EmployeeController( IUnitOfWork unitOfWork , IMapper mapper ) // work with Design Pattern Unit of Work
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue) // work async & await ...
        {
            if (string.IsNullOrEmpty(SearchValue)) 
            {   // Mapping
                var MappedEmps = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(await UnitOfWork.EmployeeRepository.GetAll());
                return View(MappedEmps);
            }
            else
            {
                var MappedEmp = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(await UnitOfWork.EmployeeRepository.SearchEmployee(SearchValue));
                return View(MappedEmp);
            }
        }
        public  IActionResult Create()
        {
            ViewBag.Departments =  UnitOfWork.DepartmentRepository.GetAll(); // Binding 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                employee.ImageName = DocumentSettings.UploadFile(employee.Image, "Imgs"); // img 
                var MappedEmp = Mapper.Map<EmployeeViewModel, Employee>(employee); // Mapping using AutoMapper pack
                await UnitOfWork.EmployeeRepository.Add(MappedEmp);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            ViewBag.Message = id;
            var MappedEmp = Mapper.Map<Employee, EmployeeViewModel>(await UnitOfWork.EmployeeRepository.Get(id));
            if (MappedEmp == null)
                return NotFound();
            return View(ViewName, MappedEmp);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Departments = UnitOfWork.DepartmentRepository.GetAll();
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel Employee)
        {
            if (id != Employee.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    DocumentSettings.DeleteFile(Employee.ImageName, "Imgs"); // to Del old Image in fold Imgs before update
                    Employee.ImageName = DocumentSettings.UploadFile(Employee.Image, "Imgs"); 
                    var MappedEmp = Mapper.Map<EmployeeViewModel, Employee>(Employee); 
                    await UnitOfWork.EmployeeRepository.Update(MappedEmp);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            ViewBag.Departments = UnitOfWork.DepartmentRepository.GetAll();
            return View(Employee);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel Employee)
        {
            if (id != Employee.Id)
                return BadRequest();
            try
            {
                DocumentSettings.DeleteFile(Employee.ImageName, "Imgs"); //  to Del file itself in fold Imgs
                var MappedEmp = Mapper.Map<EmployeeViewModel, Employee>(Employee);
                await UnitOfWork.EmployeeRepository.Delete(MappedEmp); 
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
