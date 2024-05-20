using capanna.alessandro._5H.prenota.Models;
using capanna.alessandro._5H.prenota.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace capanna.alessandro._5H.prenota.Controllers;

public class AccountController(SignInManager<Utente> signInManager, UserManager<Utente> userManager) : Controller
{
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            //login
            var result = await signInManager.PasswordSignInAsync(model.Username!, model.Password!, model.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index","Fishing");
            }

            ModelState.AddModelError("", "Invalid login attempt");
        }
        return View(model);
    }

    public IActionResult Register(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            Utente user = new()
            {
                Name = model.Name,
                UserName = model.Username,
                Email = model.Email,
                Address = model.Address
            };

            var result = await userManager.CreateAsync(user, model.Password!);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);

                return RedirectToAction("Index","Fishing");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }


     public IActionResult Delete()
    {
        return View();
    }
    public IActionResult DeleteUser()
    {
        return View();
    }

    [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteUser(LoginVM model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    Console.WriteLine("Email: " + model.Email);
    var user = await userManager.FindByEmailAsync(model.Email);
    if (user != null)
    {
        // Delete the user's account in the database
        var result = await userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
    }
    else
    {
        ModelState.AddModelError(string.Empty, "User not found");
        return View(model);
    }
}


    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index","Home");
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        return !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? Redirect(returnUrl)
            : RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
    }
}
