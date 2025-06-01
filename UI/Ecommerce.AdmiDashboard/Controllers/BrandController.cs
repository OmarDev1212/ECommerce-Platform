using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Ecommerce.AdminDashboard.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.ProductModule;
using System.Threading.Tasks;

namespace Ecommerce.AdminDashboard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BrandController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var Brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAll();

            return View(mapper.Map<IEnumerable<BrandDto>>(Brands));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandDto model)
        {
            if (!ModelState.IsValid) return View(model);

            var Brand = new ProductBrand
            {
                Name = model.Name
            };
            await unitOfWork.GetRepository<ProductBrand, int>().AddAsync(Brand);
            var result = await unitOfWork.SaveChangesAsync();
            if (result <= 0)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Brand = await unitOfWork.GetRepository<ProductBrand, int>().GetById(id);
            if (Brand is null)
                return View("NotFound");

            return View(mapper.Map<BrandDto>(Brand));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, BrandDto model)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid) return View(model);

            var Brand = await unitOfWork.GetRepository<ProductBrand, int>().GetById(model.Id);
            if (Brand is null)
                return View("NotFound");
            Brand.Name = model.Name;
             unitOfWork.GetRepository<ProductBrand, int>().Update(Brand);
            var result = await unitOfWork.SaveChangesAsync();
            if (result <= 0)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var repo = unitOfWork.GetRepository<ProductBrand, int>();
            var Brand = await repo.GetById(id);
            if (Brand is null)
                return View("NotFound");
            repo.Delete(Brand);
            var result = await unitOfWork.SaveChangesAsync();
            if (result <= 0)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
            }
            return RedirectToAction(nameof(Index));
        }

    }
}