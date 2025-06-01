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
    public class TypeController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAll();

            return View(mapper.Map<IEnumerable<TypeDto>>(types));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TypeDto model)
        {
            if (!ModelState.IsValid) return View(model);

            var Type = new ProductType
            {
                Name = model.Name
            };
            await unitOfWork.GetRepository<ProductType, int>().AddAsync(Type);
            var result = await unitOfWork.SaveChangesAsync();
            if (result <= 0)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Type = await unitOfWork.GetRepository<ProductType, int>().GetById(id);
            if (Type is null)
                return View("NotFound");

            return View(mapper.Map<TypeDto>(Type));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, TypeDto model)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid) return View(model);

            var Type = await unitOfWork.GetRepository<ProductType, int>().GetById(model.Id);
            if (Type is null)
                return View("NotFound");
            Type.Name = model.Name;
            unitOfWork.GetRepository<ProductType, int>().Update(Type);
            var result = await unitOfWork.SaveChangesAsync();
            if (result <= 0)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var repo = unitOfWork.GetRepository<ProductType, int>();
            var Type = await repo.GetById(id);
            if (Type is null)
                return View("NotFound");
            repo.Delete(Type);
            var result = await unitOfWork.SaveChangesAsync();
            if (result <= 0)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
            }
            return RedirectToAction(nameof(Index));
        }


    }
}