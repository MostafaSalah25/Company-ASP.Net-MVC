using Company.BLL.Interfaces;
using Company.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Company.Presentation_Layer.Controllers
{
    [Authorize] 
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository; // work with Design Pattern Repository  

        public DepartmentController(IDepartmentRepository departmentRepository ) 
        {
            this.departmentRepository = departmentRepository;
        }

        public IActionResult Index()  
        {
            return View(departmentRepository.GetAll());
        }
        public IActionResult Create() 
        {
            return View();
        }         
        [HttpPost] 
        public IActionResult Create(Department department)// Model Binding > take obj of model 'Department' to save in Db 
        { // from form submited 
            if (ModelState.IsValid) 
            { 
                departmentRepository.Add(department);
                TempData["Message"] = "Department is created successfully"; // Binding 
                return RedirectToAction(nameof(Index));
            }
            return View(department);  
        }

        public IActionResult Details(int? id, string ViewName = "Details") 
        { 
            if (id == null)
                return NotFound();

            ViewBag.Message = id; // Binding 

            var Department = departmentRepository.Get(id);  
            if (Department == null)
                return NotFound();
            return View(ViewName, Department);
        }
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");  
        }
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, Department department) 
        { 
            if (id != department.Id) 
                return BadRequest();
            if (ModelState.IsValid)
            {
                try 
                {
                    departmentRepository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return View(department);
        }
        public IActionResult Delete(int? id) 
        {
            return Details(id, "Delete");
        }
        [HttpPost] 
        public IActionResult Delete([FromRoute] int? id, Department department)
        {
            if (id != department.Id)
                return BadRequest();

            try
            {
                departmentRepository.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
