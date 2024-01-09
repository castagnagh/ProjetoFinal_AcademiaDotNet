﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminRolesController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<IdentityUser> _userManager;

        public AdminRolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public ViewResult Index() => View(_roleManager.Roles);

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if(result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id){
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            List<IdentityUser> members = new List<IdentityUser>();
            List<IdentityUser> nonMembers = new List<IdentityUser>();

            foreach(IdentityUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) 
                                                    ? members : nonMembers;
                list.Add(user);
            }

            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleModification model)
        {
            IdentityResult result;

            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    IdentityUser user = await _userManager.FindByIdAsync(userId);
                    if(user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach(string userId in model.DeleteIds ?? new string[] { })
                {
                    IdentityUser user = await _userManager.FindByIdAsync(userId);

                    if(user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);

                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(model.RoleId);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ModelState.AddModelError("", "Role não encontrada");
                return View("Index", _roleManager.Roles);
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);

                if(result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    Errors(result);
            }
            else
            {
                ModelState.AddModelError("", "Role não encontrada");
            }
            return View("Index", _roleManager.Roles);
        }

        private void Errors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
