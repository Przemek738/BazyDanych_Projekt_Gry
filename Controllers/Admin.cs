using Microsoft.AspNetCore.Mvc;
using BacklogManager.Data;
using BacklogManager.DTOs;

namespace BacklogManager.Controllers;
public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Users()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            var users = _context.Users
                .Select(u => new UserListViewModel
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.Role
                }).ToList();

            return View(users);
        }
        
        [HttpPost]
        public IActionResult ToggleRole(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin") return RedirectToAction("Index", "Home");

            var user = _context.Users.Find(id);
            if (user != null)
            {
                var currentUserId = HttpContext.Session.GetInt32("UserId");
                if (user.Id == currentUserId)
                {
                    TempData["WarningMessage"] = "Nie możesz zmienić swojej własnej roli!";
                    return RedirectToAction("Users");
                }

                user.Role = (user.Role == "Admin") ? "User" : "Admin";
                _context.Users.Update(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Users");
        }
        
        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin") return RedirectToAction("Index", "Home");

            var user = _context.Users.Find(id);
            if (user != null)
            {
                var currentUserId = HttpContext.Session.GetInt32("UserId");
                if (user.Id == currentUserId)
                {
                    TempData["WarningMessage"] = "Nie możesz usunąć swojego własnego konta z tego panelu!";
                    return RedirectToAction("Users");
                }

                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Users");
        }
    }