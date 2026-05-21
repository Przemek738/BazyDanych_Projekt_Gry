using Microsoft.AspNetCore.Mvc;
using BacklogManager.Data;
using BacklogManager.Models;
using BacklogManager.DTOs;

namespace BacklogManager.Controllers;
public class AuthController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Register() => View();
    
    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (_context.Users.Any(u => u.Username == model.Username))
        {
            ModelState.AddModelError("Name", "Ta nazwa użytkownika jest zajęta!");
            return View(model);
        }
        
        if (_context.Users.Any(u => u.Email == model.Email))
        {
            ModelState.AddModelError("Email", "Podany email jest już zajęty!");
            return View(model);
        }
        
        if (model.Password.Length < 6)
        {
            ModelState.AddModelError("Password", "Podane hasło musi mieć minimum 6 znaków!");
            return View(model);
        }

        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            Password = PasswordHasher.HashPassword(model.Password) 
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        return RedirectToAction("Login");
    }
    
    [HttpGet]
    public IActionResult Login() => View();
    
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        
        var hashedPassword = PasswordHasher.HashPassword(model.Password);
        var user = _context.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == hashedPassword);

        if (user != null)
        {
            HttpContext.Session.SetString("UserUsername", user.Username);
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserRole", user.Role);

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "Błędny login lub hasło!");
        return View(model);
    }
    
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
    
    // GET: /Auth/Settings
    [HttpGet]
    public IActionResult Settings()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login");

        var user = _context.Users.Find(userId);
        if (user == null) return NotFound();

        var viewModel = new ProfileSettingsViewModel
        {
            Username = user.Username,
            Email = user.Email
        };

        return View(viewModel);
    }
    
    [HttpPost]
    public IActionResult Settings(ProfileSettingsViewModel model)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login");

        var userFromDb = _context.Users.Find(userId);
        if (userFromDb == null) return NotFound();
        
        if (userFromDb.Username != model.Username && _context.Users.Any(u => u.Username == model.Username))
        {
            ModelState.AddModelError("Username", "Ta nazwa użytkownika jest już zajęta!");
            return View(model);
        }
        
        if (!string.IsNullOrEmpty(model.NewPassword))
        {
            if (string.IsNullOrEmpty(model.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "Musisz podać obecne hasło, aby ustawić nowe.");
                return View(model);
            }

            var hashedOld = PasswordHasher.HashPassword(model.OldPassword);
            if (userFromDb.Password != hashedOld)
            {
                ModelState.AddModelError("OldPassword", "Podane obecne hasło jest nieprawidłowe.");
                return View(model);
            }

            if (model.NewPassword != model.ConfirmNewPassword)
            {
                ModelState.AddModelError("ConfirmNewPassword", "Nowe hasła nie są identyczne.");
                return View(model);
            }
            
            userFromDb.Password = PasswordHasher.HashPassword(model.NewPassword);
        }
        
        userFromDb.Username = model.Username;
        userFromDb.Email = model.Email;

        _context.Users.Update(userFromDb);
        _context.SaveChanges();
        
        HttpContext.Session.SetString("UserUsername", userFromDb.Username);

        TempData["SuccessMessage"] = "Dane konta zostały pomyślnie zaktualizowane!";
        return RedirectToAction("Settings");
    }
}