using BookVertex.Business.Services;
using BookVertex.Business.Services.IServices;
using BookVertex.Models;
using BookVertex.Models.ViewModels;
using BookVertex.Utility;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookVertexWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.RoleAdmin)]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICategoryService _categoryService;
        private readonly IPhotoService _photoService;
        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment, IPhotoService photoService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
            _photoService = photoService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            ProductVM productVM = new()
            {
                CategoryList = categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                productVM.Product = await _productService.GetProductByIdAsync(id.Value);
                return View(productVM);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Upsert")]
        public async Task<IActionResult> UpsertPOST(ProductVM productVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {

                //string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    var oldPublicId = productVM.Product?.ImagePublicId;

                    //Cloudinary
                    var result = await _photoService.UploadPhotoAsync(file);

                    if (result.Error != null)
                    {
                        ModelState.AddModelError("Product.ImageUrl", result.Error.Message);
                        return View(productVM);
                    }

                    productVM.Product.ImageUrl = result.SecureUrl.AbsoluteUri;
                    productVM.Product.ImagePublicId = result.PublicId;

                    // Delete old image if one existed
                    if (!string.IsNullOrEmpty(oldPublicId))
                    {
                        await _photoService.DeletePhotoAsync(oldPublicId);
                    }

                    //Simple server pic saving 
                    /*                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                                        string productPath = Path.Combine("images", "products");
                                        string finalPath = Path.Combine(wwwRootPath, productPath);


                                        if (!Directory.Exists(finalPath))
                                            Directory.CreateDirectory(finalPath);

                                        //save the new image
                                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                                        {
                                            file.CopyTo(fileStream);
                                        }

                                        productVM.Product.ImageUrl = Path.Combine(@"\", productPath, fileName).Replace("\\", "/");*/
                }

                if (productVM.Product.Id == null || productVM.Product.Id == 0)
                {
                    //create
                    await _productService.CreateProductAsync(productVM.Product);
                }
                else
                {
                    await _productService.UpdateProductAsync(productVM.Product);

                }


                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                var categories = await _categoryService.GetAllCategoriesAsync();

                productVM = new()
                {
                    CategoryList = categories.Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    })
                };
                return View(productVM);
            }

        }







        #region API CALLS
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync(true);
            return Json(new { data = products });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new { success = false, message = "Invalid ID" });
            }

            var productToBeDeleted = await _productService.GetProductByIdAsync(id.Value);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            //delete product image if that exists in wwwroot folder
            //if (!string.IsNullOrEmpty(productToBeDeleted.ImageUrl))
            //{
            //    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\', '/'));

            //    if (System.IO.File.Exists(imagePath))
            //    {
            //        System.IO.File.Delete(imagePath);
            //    }
            //}

            // Delete image from Cloudinary
            if (!string.IsNullOrEmpty(productToBeDeleted.ImagePublicId))
            {
                var result = await _photoService.DeletePhotoAsync(
                    productToBeDeleted.ImagePublicId);

                if (result.Error != null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Unable to delete product image from Cloudinary."
                    });
                }
            }



            await _productService.DeleteProductAsync(id.Value);
            return Json(new { success = true, message = "Delete Successful" });


        }
        #endregion
    }
}
