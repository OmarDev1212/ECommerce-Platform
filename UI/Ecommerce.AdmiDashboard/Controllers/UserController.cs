using DomainLayer.Models.Identity;
using Ecommerce.AdminDashboard.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ecommerce.AdminDashboard.Controllers
{
    public class UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.Include(u => u.Address).ToListAsync();
            var usersToReturn = users.Select(u => new UserViewModel
            {
                Id = u.Id,
                DisplayName = u.DisplayName!,
                Email = u.Email!,
                PhoneNumber = u.PhoneNumber!,
                Street = u.Address?.Street ?? string.Empty,
                City = u.Address?.City ?? string.Empty,
                Country = u.Address?.Country ?? string.Empty,
                FirstName = u.Address?.FirstName ?? string.Empty,
                LastName = u.Address?.LastName ?? string.Empty,
                Roles = userManager.GetRolesAsync(u).Result,

            }).ToList();

            return View(usersToReturn);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == id);
            if (role is null)
                return View("NotFound");
            var model = new UserViewModel
            {
                DisplayName = role.DisplayName!,
                Email = role.Email!,
                PhoneNumber = role.PhoneNumber,
                Street = role.Address?.Street ?? string.Empty,
                City = role.Address?.City ?? string.Empty,
                Country = role.Address?.Country ?? string.Empty,
                Roles = await userManager.GetRolesAsync(role)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string? id, UserViewModel model)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid) return View(model);

            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == model.Id);
            if (user is null)
                return View("NotFound");
            user.Email = model.Email;
            user.DisplayName = model.DisplayName;
            user.PhoneNumber = model.PhoneNumber;
            if (user.Address == null)
            {
                user.Address = new Address();
            }
            user.Address.City = model.City;
            user.Address.Country = model.Country;
            user.Address.Street = model.Street;
            user.Address.FirstName = model.FirstName;
            user.Address.LastName = model.LastName;
            // Reassign roles
            var existingRoles = await userManager.GetRolesAsync(user);

            var removeResult = await userManager.RemoveFromRolesAsync(user, existingRoles);
            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove existing roles.");
                return View(model);
            }

            if (model.Roles != null && model.Roles.Any())
            {
                // Convert role IDs to role names
                var roleNames = await roleManager.Roles
                    .Where(r => model.Roles.Contains(r.Id))
                    .Select(r => r.Name)
                    .ToListAsync();

                var addResult = await userManager.AddToRolesAsync(user, roleNames);
                if (!addResult.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to assign new roles.");
                    return View(model);
                }
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);

        }
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return View("NotFound");

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View("Error");
        }
    }
}