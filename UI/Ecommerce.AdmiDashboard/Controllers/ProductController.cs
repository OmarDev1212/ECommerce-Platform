using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Ecommerce.AdminDashboard.Helpers;
using Ecommerce.AdminDashboard.Specifications;
using Ecommerce.AdminDashboard.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.AdminDashboard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var spec = new ProductSpecifications();
            var products = await unitOfWork.GetRepository<Product, int>().GetAll(spec);

            return View(mapper.Map<IEnumerable<ProductViewModel>>(products));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Image is not null)
            {
                var pictureurl = DocumentSettings.UploadImage(model.Image);
                model.PictureUrl = Path.Combine("images/products", pictureurl).Replace("\\", "/");
            }
            else
                model.PictureUrl = "";

            var product = mapper.Map<Product>(model);
            await unitOfWork.GetRepository<Product, int>().AddAsync(product);
            var result = await unitOfWork.SaveChangesAsync();
            if (result <= 0)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(mapper.Map<ProductViewModel>(product));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, ProductViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest("Product ID mismatch");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var product = await unitOfWork.GetRepository<Product, int>().GetById(model.Id);
            if (product == null)
            {
                return NotFound();
            }
            if (model.Image != null)
            {
                // Delete the old image if it exists
                if (model.PictureUrl != null)
                {
                    DocumentSettings.DeleteImage("products", product.PictureUrl);
                }
                var pictureurl = DocumentSettings.UploadImage(model.Image);
                model.PictureUrl = Path.Combine("images/products", pictureurl).Replace("\\", "/"); ;
            }
            mapper.Map(model, product);
            unitOfWork.GetRepository<Product, int>().Update(product);
            var result = await unitOfWork.SaveChangesAsync();
            if (result <= 0)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var repo = unitOfWork.GetRepository<Product, int>();
            var product = await repo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            repo.Delete(product);
            DocumentSettings.DeleteImage("products", product.PictureUrl);
            var result = await unitOfWork.SaveChangesAsync();
            if (result <= 0)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
