using DomainLayerCore.Interfaces;
using DomainLayerCore.Models;
using DomainLayerCore.ViewModels;
using InfrastructureLayerEF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{

    [Authorize]
    public class OrdersController : Controller
    {
        private readonly SignInManager<ApplicationUser> _siginManger;
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public OrderDetailsVM OrderDetailsViewModel { get; set; }


        public OrdersController(SignInManager<ApplicationUser> siginManger,
            UserManager<ApplicationUser> userManger, IUnitOfWork unitOfWork)
        {
            _siginManger = siginManger;
            _userManger = userManger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult OrderDetails()
        {
            var claim = _siginManger.IsSignedIn(User);
            if (claim)
            {
                var userId = _userManger.GetUserId(User);
                var CurrentUser = _unitOfWork.ApplicationUsers.GetByID(userId);
                SummaryViewModel summaryVM = new SummaryViewModel()
                {
                    CartUserId = userId,
                    UserCartList =_unitOfWork.UserCarts.GetUserCartIncludesProducts(userId),
                    OrderHeaderSummary = new UserOrderHeader(),
                    PaymentMethod = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Cash On Delivery", Value = "Cash On Delivery" },
                        new SelectListItem { Text = "Credit Card", Value = "Credit Card" },
                        new SelectListItem { Text = "PayPal", Value = "PayPal" }
                    }
                };
                if (CurrentUser != null)
                {
                    summaryVM.OrderHeaderSummary.ApplicationUser= CurrentUser;
                    summaryVM.OrderHeaderSummary.DateOfOrder= DateTime.Now;
                    summaryVM.OrderHeaderSummary.DeliveryStreet= CurrentUser.Address;
                    summaryVM.OrderHeaderSummary.PhoneNumber= CurrentUser.PhoneNumber;
                    summaryVM.OrderHeaderSummary.PostalCode= "048";
                    summaryVM.OrderHeaderSummary.City= CurrentUser.City;
                    summaryVM.OrderHeaderSummary.State= CurrentUser.State;
                    summaryVM.OrderHeaderSummary.Name= CurrentUser.FullName;

                }
                if (summaryVM.UserCartList.Count() != 0)
                {
                    return View(summaryVM);
                }else
                {
                    return RedirectToAction("Index","cart");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OrderDetails(SummaryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var claim = _siginManger.IsSignedIn(User);
                if (claim)
                {
                    var userId = _userManger.GetUserId(User);
                    var CurrentUser = _unitOfWork.ApplicationUsers.GetByID(userId);
                    SummaryViewModel summaryVM = new SummaryViewModel()
                    {
                        UserCartList = _unitOfWork.UserCarts.GetUserCartIncludesProducts(userId),
                        OrderHeaderSummary = new UserOrderHeader(),
                    };
                    if (CurrentUser != null)
                    {
                        summaryVM.OrderHeaderSummary.DateOfOrder = DateTime.Now;
                        summaryVM.OrderHeaderSummary.DeliveryStreet = model.OrderHeaderSummary.DeliveryStreet;
                        summaryVM.OrderHeaderSummary.City = model.OrderHeaderSummary.City;
                        summaryVM.OrderHeaderSummary.State = model.OrderHeaderSummary.State;
                        summaryVM.OrderHeaderSummary.Name = model.OrderHeaderSummary.Name;
                        summaryVM.SelectedPaymentMethod = model.SelectedPaymentMethod;
                        summaryVM.OrderHeaderSummary.PaymentStatus = "Not Paied";
                        summaryVM.OrderHeaderSummary.OrderStatus = "Pending";
                        summaryVM.OrderHeaderSummary.PostalCode = model.OrderHeaderSummary.PostalCode;
                        summaryVM.OrderHeaderSummary.PhoneNumber = model.OrderHeaderSummary.PhoneNumber;
                        summaryVM.OrderHeaderSummary.TotalOrderAmount = model.OrderHeaderSummary.TotalOrderAmount;
                        summaryVM.OrderHeaderSummary.UserId = userId;
                        summaryVM.OrderHeaderSummary.ApplicationUser = CurrentUser;
                        summaryVM.OrderHeaderSummary.PaymentMethod = model.SelectedPaymentMethod;
                        await _unitOfWork.UserOrderHeaders.AddAsync(summaryVM.OrderHeaderSummary);
                        await _unitOfWork.ApplyChangesAsync();
                        foreach (var product in summaryVM.UserCartList)
                        {
                            var order = new OrderDetails()
                            {
                                Count = product.Quantity,
                                OrderHeaderId = summaryVM.OrderHeaderSummary.Id,
                                Price = (float)product.Product.Price,
                                ProductId=product.Product.Id,
                                Product = product.Product,
                            };
                            await _unitOfWork.OrdersDetails.AddAsync(order);
                        }
                        var userCart = _unitOfWork.UserCarts.GetUserCarts(userId);
                       _unitOfWork.UserCarts.DeleteRange(userCart);
                        await _unitOfWork.ApplyChangesAsync();
                    }
                    if (model.OrderHeaderSummary.TotalOrderAmount>0)
                    {
                        return RedirectToAction(nameof(History));
                    }
                    else {
                        return View(model);
                    }
                }
                return View(model);
            }
            return View(model);
        }
        public IActionResult UserCartList()
        {       
            var userId = _userManger.GetUserId(User);
            var CartList = _unitOfWork.UserCarts.GetUserCartIncludesProducts(userId);
            var total = 0.0;
            foreach (var item in CartList)
            {
                item.QuantityPrice = item.Product.Price * item.Quantity;
                total += item.QuantityPrice;
            }
            return Json(new { data = CartList ,totalPrice=total});
        }



        public IActionResult OrderHistory(string? status)
        {
            var claim = _siginManger.IsSignedIn(User);
            if (claim)
            {
                var userId = _userManger.GetUserId(User);
                var orderList = new List<UserOrderHeader>();

                if (status != null)
                {
                    if (User.IsInRole("admin"))
                    {
                        orderList = _unitOfWork.UserOrderHeaders.GetOrderHeaderByStatus(status);
                    }
                    else
                    {
                        orderList = _unitOfWork.UserOrderHeaders.GetUserOrderHeadersByStatus(userId,status);
                    }
                }
                else
                {
                    if (User.IsInRole("admin"))
                    {
                        orderList = _unitOfWork.UserOrderHeaders.GetAll();
                    }
                    else
                    {
                        orderList = _unitOfWork.UserOrderHeaders.GetUserOrderHeaders(userId);
                    }
                }
                return View(orderList);
            }
            return RedirectToAction("Login", "Account");

        }
        public IActionResult History(string? status)
        {
            return View("History");
        }
        public IActionResult OrderListAll(string? status)
        {
            
                var userId = _userManger.GetUserId(User);
                var orderList = new List<UserOrderHeader>();

                if (status != null)
                {
                    if (User.IsInRole("admin"))
                    {
                    orderList = _unitOfWork.UserOrderHeaders.GetOrderHeaderByStatus(status);
                    }
                    else
                    {
                        orderList = _unitOfWork.UserOrderHeaders.GetUserOrderHeadersByStatus(userId,status);
                    }
                }
                else
                {
                    if (User.IsInRole("admin"))
                    {
                    orderList = _unitOfWork.UserOrderHeaders.GetAll();
                    }
                    else
                    {
                    orderList = _unitOfWork.UserOrderHeaders.GetUserOrderHeaders(userId);
                    }
                }
                return Json(new { data = orderList });
            

        }



        public IActionResult OrderDescription(int id)
        {
            OrderDetailsViewModel = new OrderDetailsVM();
            OrderDetailsViewModel.OrderHeader=_unitOfWork.UserOrderHeaders.GetByID(id);
            OrderDetailsViewModel.UserProductList=_unitOfWork.OrdersDetails.GetOrderDetailsIncludeProductsByHeaderId(id);
            return View(OrderDetailsViewModel);
        }
        public IActionResult UserOrderDetailsJson(int id)
        {
            var UserProductList = _unitOfWork.OrdersDetails.GetOrderDetailsIncludeProductsByHeaderId(id);
            foreach (var item in UserProductList)
            {
                item.Price = (float)item.Product.Price * item.Count;
            }
            return Json(new { data = UserProductList });
        }

        public async Task<IActionResult> UpdateOrderStatus(string status, int id, OrderDetailsVM? orderDetails)
        {
            if (orderDetails.OrderHeader != null )
            {
                id = orderDetails.OrderHeader.Id;
                status = orderDetails.OrderHeader.OrderStatus;
            }
            if (id != 0)
            {
                var orders =_unitOfWork.UserOrderHeaders.GetByID(id);
                orders.OrderStatus = status;
                if (status == "Completed")
                {
                    if (User.IsInRole("admin"))
                    {
                        var AdminId = _userManger.GetUserId(User);
                        var transiaction = new Transiaction()
                        {
                            AccountIdNum = "123456789",
                            Money = orders.TotalOrderAmount,
                            Type = orders.PaymentMethod,
                            UserId = orders.UserId,
                            AdminId = AdminId,
                        };
                        //var userCart = _context.UserCarts.FirstOrDefault(x=> x.)
                        await _unitOfWork.Transiactions.AddAsync(transiaction);
                        var account = _unitOfWork.EcommrceAccounts.GetByID(9000);
                        if (account != null)
                        {
                            account.TotalMoney += orders.TotalOrderAmount;
                        }
                    }
                    orders.PaymentProcessDate = DateTime.Now;
                }
                else if (status == "Shipped")
                {
                    orders.Carrier = orderDetails.OrderHeader.Carrier;
                    orders.TrackingNumber = orderDetails.OrderHeader.TrackingNumber;
                    orders.DateOfShipped = DateTime.Now;
                }
                await _unitOfWork.ApplyChangesAsync();
                return RedirectToAction(nameof(OrderDescription), new { id = id });
            }
            TempData["AlertMessage"] = "Insert Values in Carrier and Tracking Number";
            return RedirectToAction("OrderDescription", new { id = orderDetails.OrderHeader.Id });
        }
    }
}
