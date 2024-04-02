﻿using Cafe_Site.Models;
using Cafe_Site.Repository;
using Cafe_Site.Services;
using Cafe_Site.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Cafe_Site.Controllers
{
    [Authorize(Roles = "Admin")]
    //[Authorize]
    public class AdminDashboardController : Controller
	{
        private readonly IProductService productService;
        //private readonly RoleManager<IdentityRole> roleManager;
        //private readonly UserManager<ApplicationUser> userManager;

        public AdminDashboardController(IProductService productService)
        {
            this.productService = productService;
            //this.roleManager = roleManager;
            //this.userManager = userManager;
        }

        public IActionResult Index()
		{
            //var user = await userManager.GetUserAsync(User);

            //var test = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //var test2 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            //var test3 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            //var test4 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            //try
            //{
            //    var flag = await userManager.GetRolesAsync(user);

            //    if (flag[0] == "Admin")
            //    {
            var products = productService.GetAllProducts();

                    return View("Index", products);
            //    }
            //    else
            //    {
            //        //return View("Error", new ErrorViewModel() { RequestId = "Access Denied" });
            //        return Content("Access Denied");
            //    }
            //}
            //catch (Exception)
            //{
            //    //return View("Error", new ErrorViewModel() { RequestId = "Access Denied" });
            //    return Content("Access Denied");
            //}

            //var flag = User.IsInRole("Admin");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public IActionResult Add(ProductInfoViewModel product)
        {
            if (ModelState.IsValid)
            {
                productService.InsertProduct(product, User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

                return RedirectToAction("Index");
            }

            return View("Add", product);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = productService.GetProduct(id);

            return View("Update", product);
        }

        [HttpPost]
        public IActionResult Update(ProductInfoViewModel product)
        {
            if (ModelState.IsValid)
            {
                productService.UpdateProduct(product);

                return RedirectToAction("Index");
            }

            return View("Update", product);
        }

        public IActionResult Delete(int id)
        {
            productService.DeleteProduct(id);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteSize(int id, char size)
        {
            productService.DeleteSize(id, size);

            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public IActionResult AddRole()
        //{
        //    return View("AddRole");
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddRole(RoleViewModel newRoleVM)
        //{
        //    if (ModelState.IsValid == true)
        //    {
        //        IdentityRole roleModel = new IdentityRole()
        //        {
        //            Name = newRoleVM.RoleName
        //        };
        //        IdentityResult rust = await roleManager.CreateAsync(roleModel);

        //    }
        //    return View("AddRole");
        //}

        //public async Task<IActionResult> AssignRole()
        //{
        //    //var user = productService.GetUser(User.Claims.FirstOrDefault().Value);

        //    var user = await userManager.GetUserAsync(User);

        //    IdentityResult resultRole = await userManager.AddToRoleAsync(user, "Admin");

        //    return RedirectToAction("Index");
        //}
    }
}
