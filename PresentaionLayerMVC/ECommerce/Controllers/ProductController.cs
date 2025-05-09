﻿using DomainLayerCore.Interfaces;
using DomainLayerCore.Models;
using DomainLayerCore.ViewModels;
using ECommerce.Utility;
using InfrastructureLayerEF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDBContext _context;
        //private readonly GenericRepository<Product> _product;
        //private readonly GenericRepository<Category> _category;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ApplicationDBContext context
            , SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;

        }

        public IActionResult Index()
        {
            return View(_unitOfWork.Products.GetAllWithCategory());
        }
        public IActionResult All(string ? SearchName = null)
        {
            var claim = _signInManager.IsSignedIn(User);
            if (claim != null)
            {
                var userId   = _userManager.GetUserId(User);
                var quantity = _unitOfWork.UserCarts.GetUserCarts(userId);
                var count = 0;
                foreach (var item in quantity)
                {
                    count += item.Quantity;
                }
                HttpContext.Session.SetInt32(CartCount.sessionCount, count);
            }


            ProductVM VM = new ProductVM();
            if(SearchName != null)
            {
                VM.categories=_unitOfWork.Categories.GetAll();
                VM.products = _unitOfWork.Products.GetProductsByName(SearchName);
               
                return View(VM);
            }
            else
            {
                VM.products = _unitOfWork.Products.GetAll();
                VM.categories = _unitOfWork.Categories.GetAll();
                return View(VM);
            }
            
        }

        public IActionResult Details(int Id)
        {
            return View (_unitOfWork.Products.GetByIdWithCategory(Id));
        }


        [HttpGet]
        [Authorize(Roles ="admin")]
        public IActionResult Create()
        {   
            ProductViewModel productVM = new ProductViewModel()
            {
                Inventories = new Inventory (),
                pImages = new PImages (),
                CategoriesList = _unitOfWork.Categories.GetAll()
            };
            return View(productVM);
                    }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productVM)
        {
            string HomeImageUrl = "";
            _unitOfWork.Products.Add(productVM.Product);
            _unitOfWork.ApplyChanges();
            
            var newProduct = _unitOfWork.Products.GetByID(productVM.Product.Id);
            if (productVM.Images != null)
            {
                foreach (var image in productVM.Images)
                {
                    HomeImageUrl = Uploadfiles(image);
                    var addressImage = new PImages
                    {
                        ImageUrl = HomeImageUrl,
                        ProductId = newProduct.Id,
                        ProductName = newProduct.Name,
                    };
                    await _unitOfWork.PImages.AddAsync(addressImage);
                }
            }
            productVM.Product.HomeImageUrl = HomeImageUrl;
            productVM.Inventories.Name= newProduct.Name;
            productVM.Inventories.Category = _unitOfWork.Categories.GetByID(newProduct.CategoryId).Name;
            await _unitOfWork.Inventories.AddAsync(productVM.Inventories);
            await _unitOfWork.ApplyChangesAsync();
            return RedirectToAction("Index","Product");
        }

        private string Uploadfiles(IFormFile image)
        {
            string fileName = null;
            if (image != null)
            {
                string uploadDirLocation = Path.Combine(_webHostEnvironment.WebRootPath,"Images");
                fileName = Guid.NewGuid().ToString()+"_"+image.FileName;
                string filePath = Path.Combine(uploadDirLocation,fileName);
                using(var fileStream = new FileStream(filePath,FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            return fileName;
        }





        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            ProductViewModel productVM = new ProductViewModel()
            {
                Product = _unitOfWork.Products.GetByID(id),
                CategoriesList = _unitOfWork.Categories.GetAll()
            };
            productVM.Product.ImageUrls = _unitOfWork.PImages.GetProductImages(id);
            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id ,ProductViewModel productVM)
        {
            var oldProduct = _unitOfWork.Products.GetByID(id);
            var stringfilename = "";
            if (oldProduct != null)
            {
                oldProduct.Id = id;
                oldProduct.Price=productVM.Product.Price;
                oldProduct.Description=productVM.Product.Description;
                oldProduct.Name=productVM.Product.Name;
                oldProduct.CategoryId=productVM.Product.CategoryId;
                if (productVM.Images != null)
                {
                    foreach (var image in productVM.Images)
                    {
                         stringfilename = Uploadfiles(image);
                        var addressImage = new PImages
                        {
                            ImageUrl = stringfilename,
                            ProductId = id,
                            ProductName = productVM.Product.Name,
                        };
                        await _unitOfWork.PImages.AddAsync(addressImage);
                    }
                }
                if (oldProduct.HomeImageUrl == null || oldProduct.HomeImageUrl == "noimage.png")
                {
                    var image = _unitOfWork.PImages.GetProductImages(id).FirstOrDefault();
                    var imageURl = "noimage.png";
                    if (image!=null)
                    {
                        imageURl = image.ImageUrl;
                    }
                    oldProduct.HomeImageUrl = stringfilename != "" ? stringfilename : imageURl;

                }
            }
        _unitOfWork.Products.Update(oldProduct);
        _unitOfWork.ApplyChanges();
        return RedirectToAction("Index");
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            Product productVM = _unitOfWork.Products.GetByID(id);
            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id , Product product)
        {
            Product productVM = _unitOfWork.Products.GetByID(id);
            var images = _unitOfWork.PImages.GetProductImages(id);
            foreach (var image in images)
            {
                string imageurl = "Images\\" + image.ImageUrl;
                var toDeleteImageFromFolder = Path.Combine(_webHostEnvironment.WebRootPath, imageurl.Trim('\\'));
                DeleteImage(toDeleteImageFromFolder);
            }
            if (productVM.HomeImageUrl != "")
            {
                string imageurl = "Images\\" + productVM.HomeImageUrl;
                var toDeleteImageFromFolder = Path.Combine(_webHostEnvironment.WebRootPath, imageurl.Trim('\\'));
                DeleteImage(toDeleteImageFromFolder);
            }
            _unitOfWork.Products.Delete(id);
            return RedirectToAction("Index", "Product");
        }

        private void DeleteImage(string toDeleteImageFromFolder)
        {
            if (System.IO.File.Exists(toDeleteImageFromFolder))
            {
                System.IO.File.Delete(toDeleteImageFromFolder);
            } 
        }


        public IActionResult DeleteImageFromProduct(string url)
        {
            if (url != null)
            {
                var product = _unitOfWork.Products.GetByHomeImage(url);
                var id = 0;
                if (product != null) 
                {
                    var image1 = _unitOfWork.PImages.GetProductImages(product.Id).First().ImageUrl;
                    var image2 = _unitOfWork.PImages.GetProductImages(product.Id).Last().ImageUrl;
                    image2 = image1 == image2 ? null : image2;
                    product.HomeImageUrl =  image1 == product.HomeImageUrl ? image2 : image1;
                   _unitOfWork.Products.Update(product);
                }

                var image = _unitOfWork.PImages.GetPImagesByImageUrl(url);
                id = image.ProductId;
                _unitOfWork.PImages.Delete(image);
                
                string imgUrl = "Images\\" + url;
                var toDeleteImage = Path.Combine(_webHostEnvironment.WebRootPath, imgUrl.Trim('\\'));
                DeleteImage(toDeleteImage); 
                _unitOfWork.ApplyChanges();
                return Redirect("/Product/Edit/"+id);
            }   
            return Content("can not delete this image");
        }
    }
    }
