using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FishyBuisness_3.Controllers
{
    public class AppRolesController : Controller

    {

        private readonly RoleManager<IdentityRole> _roleManager;

        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        // List all the roles created by Users
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }



        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

    

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
    //    [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(IdentityRole model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {

                return RedirectToAction("Index");
            }

            IdentityRole role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {

                return RedirectToAction("Index");
            }

            IdentityResult result;
            try
            {
                result = await _roleManager.DeleteAsync(role);
            }
            catch (System.Exception ex)
            {

                return RedirectToAction("Index");
            }

            if (result.Succeeded)
            {
                // Message for user
            }
            else
            {
                // Handle the case where the delete operation failed
                // Message for user
            }

            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("Index");
            }

            IdentityResult result;
            try
            {
                result = await _roleManager.DeleteAsync(role);
            }
            catch (System.Exception ex)
            {
              
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the role.");
                return View("Delete", role); // Return to the Delete view with the model for correction
            }

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Role deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete the role.";
            }

            return RedirectToAction("Index");
        }
    }
}
