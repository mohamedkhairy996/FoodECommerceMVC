using DomainLayerCore.Interfaces;
using DomainLayerCore.Models;
using DomainLayerCore.ViewModels;
using InfrastructureLayerEF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationMVC01.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            this.roleManager = roleManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(RoleViewModel userRole)
        {
            //IdentityRole role = await roleManager.FindByNameAsync(userRole.RoleName);
            if (ModelState.IsValid )
            {
                IdentityRole newRole = new IdentityRole();
                newRole.Name=userRole.RoleName;

              IdentityResult result =  await roleManager.CreateAsync(newRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        
                    }
                    return View(userRole);
                }
            }
            else
            {
                return View(userRole);
            }
        }


        [HttpGet]
        public IActionResult AddRole()
        {
            var addRoleToUser = new AddRoleToUserViewModel() {

                Users = _unitOfWork.ApplicationUsers.GetAll(),
                Roles = _unitOfWork.Roles.GetAll(),
                };

            return View(addRoleToUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole (AddRoleToUserViewModel user_Role)
        {
            //IdentityRole role = await roleManager.FindByNameAsync(userRole.RoleName);
            if (ModelState.IsValid)
            {
                var user = _unitOfWork.ApplicationUsers.GetByID(user_Role.SelectedUser);
                if (user != null && user_Role.SelectedRole != null)
                {
                    //    var users = await _userManager.FindByNameAsync(User.Identity.Name);
                    var roles = await _userManager.GetRolesAsync(user);

                    if (!roles.Contains(user_Role.SelectedRole))
                    {
                        await _userManager.AddToRoleAsync(user, user_Role.SelectedRole);
                        return Content($"Role Add Successfully -- {user.FullName} Become  {user_Role.SelectedRole} :)");
                    }
                    return Content("Role assigned before");
                }
                else
                {
                    ModelState.AddModelError("", "Select User and Role");
                    return View(user_Role);
                }
            }
            else
            {
                return View(user_Role);
            }
        }
    }
}
