using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BacklogManager.Data;
using BacklogManager.Models;
using BacklogManager.DTOs;

namespace BacklogManager.Controllers;
public class UserGamesController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public UserGamesController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            TempData["WarningMessage"] = "Wymaga zalogowanego konta!";
            
            return RedirectToAction("Login", "Auth");
        };

        var myGames = _context.UserGames
            .Include(ug => ug.Game)
            .Include(ug => ug.Platforms)
            .Where(ug => ug.UserId == userId)
            .Select(ug => new UserGameIndexViewModel {
                UserGameId = ug.Id,
                GameId = ug.GameId,
                GameTitle = ug.Game.Title,
                PlatformNames = ug.Platforms.Select(p => p.Name).ToList(),
                Status = ug.Status,
                Rating = ug.Rating,
                ImageUrl = ug.Game.ImageUrl,
                Notes = ug.Notes
            }).ToList();

        return View(myGames);
    }
    
    [HttpPost]
    public IActionResult QuickAdd(int gameId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) 
        {
            return Json(new { success = false, message = "Musisz się zalogować!" });
        }
        var alreadyExists = _context.UserGames.Any(ug => ug.UserId == userId && ug.GameId == gameId);
        if (alreadyExists) 
        {
            return Json(new { success = false, message = "Ta gra jest już w Twojej bibliotece." });
        }
        var newUserGame = new UserGame {
            UserId = (int)userId,
            GameId = gameId,
            Status = "Planuję zagrać",
            Notes = ""
        };

        _context.UserGames.Add(newUserGame);
        _context.SaveChanges();

        return Json(new { success = true });
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var userGame = _context.UserGames.Find(id);
        if (userGame != null)
        {
            _context.UserGames.Remove(userGame);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");
        var userGame = _context.UserGames
            .Include(ug => ug.Game)
            .Include(ug => ug.Platforms) 
            .FirstOrDefault(ug => ug.Id == id && ug.UserId == userId);
            
        if (userGame == null) return NotFound();

        var viewModel = new AddToLibraryViewModel {
            GameId = userGame.GameId,
            GameTitle = userGame.Game.Title,
            SelectedPlatformIds = userGame.Platforms.Select(p => p.Id).ToList(), 
            AvailablePlatforms = _context.Platforms.ToList(),
            
            Status = userGame.Status,
            Notes = userGame.Notes
        };
        ViewBag.UserGameId = userGame.Id; 
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Edit(int id, AddToLibraryViewModel model)
    {
        var userGame = _context.UserGames
            .Include(ug => ug.Platforms) 
            .FirstOrDefault(ug => ug.Id == id);

        if (userGame != null)
        {
            var selectedPlatforms = _context.Platforms
                .Where(p => model.SelectedPlatformIds.Contains(p.Id))
                .ToList();

            userGame.Platforms = selectedPlatforms;
            userGame.Status = model.Status;
            userGame.Notes = model.Notes;
        
            _context.UserGames.Update(userGame);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
} 