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
        SeedAdminAccount();
    }
    
    private void SeedAdminAccount()
    {
        if (!_context.Users.Any(u => u.Username == "admin"))
        {
            var admin = new User
            {
                Username = "admin",
                Email = "admin@backlog.pl",
                Password = PasswordHasher.HashPassword("admin123"),
                Role = "Admin"
            };
            _context.Users.Add(admin);
            _context.SaveChanges();
        }
    }
    
    [HttpGet]
    public IActionResult Register() => View();
    
    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (_context.Users.Any(u => u.Username == model.Username))
        {
            ModelState.AddModelError("", "Ta nazwa użytkownika jest zajęta!");
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
}