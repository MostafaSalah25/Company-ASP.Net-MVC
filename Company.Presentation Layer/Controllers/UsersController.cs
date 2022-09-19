using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Entities;
using Company.Presentation_Layer.Helper;
using Company.Presentation_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Presentation_Layer.Controllers
{
    [Authorize] 
    public class UsersController : Controller
    {
        
        private readonly UserManager<ApplicationUser> userManager;
        public UsersController(UserManager<ApplicationUser> userManager) 
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string SearchValue) 
        {
            if (string.IsNullOrEmpty(SearchValue))  
            {

                return View(userManager.Users); 
            }
            else
            {
                var user = await userManager.FindByEmailAsync(SearchValue);
                if (user != null)
                     return View( new List<ApplicationUser>() { user } ); 
                return View(new List<ApplicationUser>() {  });
            }
        }
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            ViewBag.Message = id;
            var user = await userManager.FindByIdAsync(id); 
            if (user == null)
                return NotFound();
            return View( ViewName, user );
        }
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, ApplicationUser updatedUser)
        {
            if (id != updatedUser.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userManager.FindByIdAsync(id);
                    //  first get it from Db then edit >
                    user.UserName = updatedUser.UserName;
                    user.PhoneNumber = updatedUser.PhoneNumber;
                    user.NormalizedUserName = updatedUser.UserName.ToUpper();


                    await userManager.UpdateAsync(user); // update user
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception  ex)
                {
                    throw; 
                }
            }
            return View(updatedUser);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string id, ApplicationUser deletedUser)
        {
            if (id != deletedUser.Id)
                return BadRequest();
            try
            {
                var user = await userManager.FindByIdAsync(deletedUser.Id);
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw;
            }
        }



    }
}
