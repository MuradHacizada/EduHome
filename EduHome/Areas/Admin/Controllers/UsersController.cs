using EduHome.Helper;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly SignInManager<AppUser> _signInManager;
        public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)// SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            //_signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            List<AppUser> dbUsers = await _userManager.Users.ToListAsync();
            List<UserVM> usersVm = new List<UserVM>();
            foreach (AppUser dbUser in dbUsers)
            {
                UserVM userVm = new UserVM()
                {
                    Id = dbUser.Id,
                    Name = dbUser.Name,
                    Surname = dbUser.Surname,
                    Username = dbUser.UserName,
                    Email = dbUser.Email,
                    IsDeactive = dbUser.IsDeactive,
                    Role = (await _userManager.GetRolesAsync(dbUser))[0]
                };
                usersVm.Add(userVm);
            }
            return View(usersVm);
        }

        #region Create
        public IActionResult Create()
        {
            ViewBag.Roles = new List<string>
            {
                Roles.Admin.ToString(),
                Roles.Member.ToString()
            };
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create(CreateeVM createVM, string role)
        {
            ViewBag.Roles = new List<string>
            {
                Roles.Admin.ToString(),
                Roles.Member.ToString()
            };
            AppUser newUser = new AppUser
            {
                UserName = createVM.Username,
                Name = createVM.Name,
                Surname = createVM.Surname,
                Email = createVM.Email,
            };
            IdentityResult identityResult = await _userManager.CreateAsync(newUser, createVM.Password);
            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(newUser, role);

            return RedirectToAction("Index");
        } 
        #endregion
        

        #region Update
        public async Task<IActionResult> Update(string id)
        {
            #region Get
            if (id == null)
            {
                return NotFound();
            }
            AppUser dbUser = await _userManager.FindByIdAsync(id);
            if (dbUser == null)
            {
                return BadRequest();
            }
            UpdateVM dbUpdateVM = new UpdateVM
            {
                Name = dbUser.Name,
                Username = dbUser.UserName,
                Surname = dbUser.Surname,
                Email = dbUser.Email,
                Role = (await _userManager.GetRolesAsync(dbUser))[0],
            };
            ViewBag.Roles = new List<string>
            {
                Roles.Admin.ToString(),
                Roles.Member.ToString()
            }; 
            #endregion
            return View(dbUpdateVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id,UpdateVM updateVM,string role)
        {
            #region Get
            if (id == null)
            {
                return NotFound();
            }
            AppUser dbUser = await _userManager.FindByIdAsync(id);
            if (dbUser == null)
            {
                return BadRequest();
            }
            UpdateVM dbupdateVM = new UpdateVM
            {
                Name = dbUser.Name,
                Username = dbUser.UserName,
                Surname = dbUser.Surname,
                Email = dbUser.Email,
                Role = (await _userManager.GetRolesAsync(dbUser))[0],
            };
            ViewBag.Roles = new List<string>
            {
                Roles.Admin.ToString(),
                Roles.Member.ToString()
            };
            #endregion

            dbUser.Name = updateVM.Name;
            dbUser.Surname = updateVM.Surname;
            dbUser.UserName = updateVM.Username;
            dbUser.Email = updateVM.Email;

            if (dbupdateVM.Role != role)
            {
               IdentityResult removeidentityResult= await _userManager.RemoveFromRoleAsync(dbUser, dbupdateVM.Role);
                if (!removeidentityResult.Succeeded)
                {
                    foreach (IdentityError error in removeidentityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }
                IdentityResult addidentityResult = await _userManager.AddToRoleAsync(dbUser,role);
                if (!addidentityResult.Succeeded)
                {
                    foreach (IdentityError error in addidentityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }
            }

            await _userManager.UpdateAsync(dbUser);
            return RedirectToAction("Index");
        }

        #endregion

        #region Activiy
        public async Task<IActionResult> Activity(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser dbUser = await _userManager.FindByIdAsync(id);
            if (dbUser == null)
            {
                return BadRequest();
            }
            if (dbUser.IsDeactive)
            {
                dbUser.IsDeactive = false;
            }
            else
            {
                dbUser.IsDeactive = true;
            }
            await _userManager.UpdateAsync(dbUser);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
