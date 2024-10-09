using DomainLayerCore.Models;
using DomainLayerCore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace ECommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager , UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Login( string returnUrl=null)
        {
            ViewBag.returnUrl = returnUrl;
            return View(new LoginUserViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel LoginUser , string returnUrl = null)
       {
            var user1 = await _userManager.FindByEmailAsync(LoginUser.Email);
            var user2 = await _userManager.FindByNameAsync(LoginUser.Email);
            if (user1 != null)
            {
                bool isTrueUser = await _userManager.CheckPasswordAsync(user1, LoginUser.Password);
                if (isTrueUser)
                {
                    await _signInManager.SignInAsync(user1, isPersistent: LoginUser.RememberMe);
                    if(returnUrl != null && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl); 
                    }
                
                    return RedirectToAction("All", "Product");

                }
            }
            else if (user2 != null)
            {
                bool isTrueUser = await _userManager.CheckPasswordAsync(user2,LoginUser.Password);
                if (isTrueUser)
                {
                    await _signInManager.SignInAsync(user2, isPersistent: LoginUser.RememberMe);
                    if (returnUrl != null && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("All", "Product");

                }
            }
            ModelState.AddModelError("", "UserName Or Password Is not correct");
            return View(LoginUser);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterUserViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel NewUser)
        {
            var exist = await _userManager.FindByEmailAsync(NewUser.Email);
            if (exist == null)
            {
                var user = new ApplicationUser
                {
                    Email = NewUser.Email,
                    PasswordHash = NewUser.Password,
                    UserName = NewUser.UserName,
                    Address = NewUser.applicationUser.Address,
                    PhoneNumber = NewUser.applicationUser.PhoneNumber,
                    City = NewUser.applicationUser.City,
                    Country = NewUser.applicationUser.Country,
                    FirstName = NewUser.applicationUser.FirstName,
                    LastName = NewUser.applicationUser.LastName,
                    State = NewUser.applicationUser.State
                };
                IdentityResult created = await _userManager.CreateAsync(user, NewUser.Password);
                if (created.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("All", "Product");
                }
                else
                {
                    foreach (var item in created.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                   
                    return View(NewUser);
                }
            }
            else
            {
                ModelState.AddModelError("", "UserName or Email Is Existed before ");
                return View(NewUser);
            }
        }


        
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        
        
    }
}
