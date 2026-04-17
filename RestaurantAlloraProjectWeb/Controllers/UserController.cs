using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantAlloraProject.ViewModels.User;
using RestaurantAlloraProjectData.Entities;

namespace RestaurantAlloraProjectWeb.Controllers
{
    public class UserController : Controller
    {
        private static readonly string[] ManageableRoles = { "Employee", "Customer" };

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        public UserController(
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            RoleManager<IdentityRole<Guid>> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var roles = new string[] { "Customer", "Employee" };
            ViewBag.Roles = new SelectList(roles);

            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var roles = new string[] { "Customer", "Employee" };

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(roles);
                return View(model);
            }

            if (!roles.Contains(model.Role))
            {
                ModelState.AddModelError(nameof(model.Role), "Невалидна роля.");
                ViewBag.Roles = new SelectList(roles);
                return View(model);
            }

            var user = new User()
            {
                Email = model.EmailAddress,
                UserName = model.UserName,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(user, model.Role);

                if (!roleResult.Succeeded)
                {
                    foreach (var item in roleResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                    ViewBag.Roles = new SelectList(roles);
                    return View(model);
                }

                return RedirectToAction("LogIn", "User");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            ViewBag.Roles = new SelectList(roles);
            return View(model);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid Login");
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.EmailAddress);

            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                ViewBag.ResetPasswordUrl = Url.Action(
                    nameof(ResetPassword),
                    "User",
                    new { emailAddress = model.EmailAddress, token },
                    Request.Scheme);
            }

            return View("ForgotPasswordConfirmation");
        }

        [HttpGet]
        public IActionResult ResetPassword(string emailAddress, string token)
        {
            if (string.IsNullOrWhiteSpace(emailAddress) || string.IsNullOrWhiteSpace(token))
            {
                return RedirectToAction(nameof(LogIn));
            }

            return View(new ResetPasswordViewModel
            {
                EmailAddress = emailAddress,
                Token = token
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.EmailAddress);

            if (user == null)
            {
                return View("ResetPasswordConfirmation");
            }

            var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("LogIn", "User");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Manage()
        {
            var currentUserId = userManager.GetUserId(User);

            var users = userManager.Users
                .Where(u => currentUserId == null || u.Id.ToString() != currentUserId)
                .OrderBy(u => u.UserName)
                .ToList();

            var model = new List<UserListItemViewModel>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);

                model.Add(new UserListItemViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    Role = roles.FirstOrDefault() ?? "-"
                });
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id.ToString() == userManager.GetUserId(User))
            {
                TempData["UserError"] = "Собственият профил не се управлява от този списък.";
                return RedirectToAction(nameof(Manage));
            }

            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            var roles = await userManager.GetRolesAsync(user);
            var selectedRole = roles.FirstOrDefault() ?? "Customer";

            ViewBag.Roles = new SelectList(ManageableRoles, selectedRole);

            return View(new UserEditViewModel
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Role = selectedRole
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (model.Id.ToString() == userManager.GetUserId(User))
            {
                TempData["UserError"] = "Собственият профил не се управлява от този списък.";
                return RedirectToAction(nameof(Manage));
            }

            if (!ManageableRoles.Contains(model.Role))
            {
                ModelState.AddModelError(nameof(model.Role), "Невалидна роля.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(ManageableRoles, model.Role);
                return View(model);
            }

            var user = await userManager.FindByIdAsync(model.Id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await userManager.GetRolesAsync(user);

            if (currentRoles.Contains("Admin") && model.Role != "Admin" && await IsLastAdminAsync(user))
            {
                ModelState.AddModelError(nameof(model.Role), "Не може да премахнете ролята на последния администратор.");
                ViewBag.Roles = new SelectList(ManageableRoles, model.Role);
                return View(model);
            }

            user.UserName = model.UserName.Trim();
            user.Email = model.Email.Trim();

            var updateResult = await userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                AddIdentityErrors(updateResult);
                ViewBag.Roles = new SelectList(ManageableRoles, model.Role);
                return View(model);
            }

            if (currentRoles.Any())
            {
                var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);

                if (!removeResult.Succeeded)
                {
                    AddIdentityErrors(removeResult);
                    ViewBag.Roles = new SelectList(ManageableRoles, model.Role);
                    return View(model);
                }
            }

            var roleResult = await userManager.AddToRoleAsync(user, model.Role);

            if (!roleResult.Succeeded)
            {
                AddIdentityErrors(roleResult);
                ViewBag.Roles = new SelectList(ManageableRoles, model.Role);
                return View(model);
            }

            TempData["UserSuccess"] = "Потребителят е обновен.";
            return RedirectToAction(nameof(Manage));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return RedirectToAction(nameof(Manage));
            }

            if (user.Id.ToString() == userManager.GetUserId(User))
            {
                TempData["UserError"] = "Не можете да изтриете собствения си профил.";
                return RedirectToAction(nameof(Manage));
            }

            if (await userManager.IsInRoleAsync(user, "Admin") && await IsLastAdminAsync(user))
            {
                TempData["UserError"] = "Не можете да изтриете последния администратор.";
                return RedirectToAction(nameof(Manage));
            }

            var result = await userManager.DeleteAsync(user);

            TempData[result.Succeeded ? "UserSuccess" : "UserError"] = result.Succeeded
                ? "Потребителят е изтрит."
                : string.Join(" ", result.Errors.Select(e => e.Description));

            return RedirectToAction(nameof(Manage));
        }

        private async Task<bool> IsLastAdminAsync(User user)
        {
            var admins = await userManager.GetUsersInRoleAsync("Admin");
            return admins.Count == 1 && admins.Any(admin => admin.Id == user.Id);
        }

        private void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
