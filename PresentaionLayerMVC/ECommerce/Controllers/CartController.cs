using DomainLayerCore.Interfaces;
using DomainLayerCore.Models;
using DomainLayerCore.ViewModels;
using InfrastructureLayerEF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;



        public CartController(SignInManager<ApplicationUser> signInManager,
                                UserManager<ApplicationUser> userManager,
                                IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var cartVM =new CartVM();
            var checkIfSignedIn = _signInManager.IsSignedIn(User);
            if (checkIfSignedIn)
            {
                var user = _userManager.GetUserId(User);
                if (user != null)
                {
                    //Check if the signed user has any cart or not?
                    var CartForTheUser = await _unitOfWork.UserCarts.GetUserCartsAsync(user);
                    
                    foreach (var item in CartForTheUser)
                    {
                        var product = _unitOfWork.Products.GetByID(item.ProductId.Value);
                        cartVM.Products.Add(product);
                        cartVM.Quantity.Add($"{product.Name}", item.Quantity);
                        cartVM.QuantityPrice.Add($"{product.Name}", item.Quantity * product.Price);
                    }
                    return View(cartVM);
                }
                return View( new UserCart());
            }
            else
            {
               
                return View(new UserCart());
            }
        }
        
        public async Task<IActionResult> AddToCart(int Id)
        {
            if (Id != 0)
            {
                var productToAdd = _unitOfWork.Products.GetByID(Id);
                var checkIfSignedIn = _signInManager.IsSignedIn(User);
                if (checkIfSignedIn)
                {
                    var user = _userManager.GetUserId(User);
                    if (user != null)
                    {
                        //Check if the signed user has any cart or not?
                        var getTheCartIfAnyExistForTheUser = await _unitOfWork.UserCarts.GetUserCartsAsync(user);
                        if (getTheCartIfAnyExistForTheUser.Count() > 0)
                        {
                            //check if the item is already in the cart or not
                            var getTheQuantity = getTheCartIfAnyExistForTheUser.FirstOrDefault(p => p.ProductId == Id);
                            if (getTheQuantity != null)
                            { //if the item is already in the cart just increase the quantity by 1 and update the cart.
                                getTheQuantity.Quantity = getTheQuantity.Quantity + 1;
                                _unitOfWork.UserCarts.Update(getTheQuantity);
                            }
                            else
                            { // User has a cart but addding a new item to the existing cart.
                                UserCart newItemToCart = new UserCart
                                {
                                    ProductId = Id,
                                    UserId = user,
                                    Quantity = 1
                                };
                                await _unitOfWork.UserCarts.AddAsync(newItemToCart);
                            }
                        }
                        else
                        {
                            // User has no cart. Addding a brand new cart for the user.
                            UserCart newItemToCart = new UserCart
                            {
                                ProductId = Id,
                                UserId = user,
                                Quantity = 1,
                            };

                            await _unitOfWork.UserCarts.AddAsync(newItemToCart);

                        }
                    }
                }
                //var x =_context.EcommerceAccounts.FirstOrDefault(x => x.Id == 9000);
                //x.TotalMoney = productToAdd.Price + x.TotalMoney;
                await _unitOfWork.ApplyChangesAsync();
                return Json(new { success = "true" });
            }return RedirectToAction("All","Product");
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var editCart = new EditCartVM();
            editCart.Product = _unitOfWork.Products.GetByID(Id);
            var checkIfSignedIn = _signInManager.IsSignedIn(User);
            if (checkIfSignedIn)
            {
                var user = _userManager.GetUserId(User);
                if (user != null)
                {
                    var cart = _unitOfWork.UserCarts.GetUserCartForProduct(user, Id);
                    editCart.Quantity = cart.Quantity;
                    editCart.QuantityPrice = cart.Quantity * editCart.Product.Price;
                }
                return View(editCart);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] EditProductViewModel product)
        {
        var productToEdit = _unitOfWork.Products.GetByID(product.Id);
        var checkIfSignedIn = _signInManager.IsSignedIn(User);
        if (checkIfSignedIn)
        {
            var user = _userManager.GetUserId(User);
            if (user != null)
            {
                //Check if the signed user has any cart or not?
                var getTheCartIfAnyExistForTheUser = await _unitOfWork.UserCarts.GetUserCartsAsync(user);
                if (getTheCartIfAnyExistForTheUser.Count() > 0)
                {
                    //check if the item is already in the cart or not
                    var getTheQuantity = getTheCartIfAnyExistForTheUser.FirstOrDefault(p => p.ProductId == product.Id);
                    if (product .Quantity> 0)
                    { //if the item is already in the cart just increase the quantity by 1 and update the cart.
                        getTheQuantity.Quantity = product.Quantity;
                        _unitOfWork.UserCarts.Update(getTheQuantity);
                    }
                    else
                    { // User has a cart but remove an existed item .
                        _unitOfWork.UserCarts.Delete(getTheQuantity);
                    }
                }
             }
        }
        await _unitOfWork.ApplyChangesAsync();
            return Json(new
            {
                success = true,
                msg = "Editted successfull",

            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Purshase() 
        {
            return View("Index");
        }
    }
}
