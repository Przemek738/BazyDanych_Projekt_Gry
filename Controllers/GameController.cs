using Microsoft.AspNetCore.Mvc;
using BacklogManager.Data;
using BacklogManager.DTOs;
using BacklogManager.Models;

namespace BacklogManager.Controllers;
public class GameController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public GameController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Index(string searchString, string selectedGenre)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var userOwnedGameIds = new List<int>();
        
        if (userId != null)
        {
            userOwnedGameIds = _context.UserGames
                .Where(ug => ug.UserId == userId)
                .Select(ug => ug.GameId)
                .ToList();
        }
        var gamesListDto = _context.Games
            .Select(game => new GameCreateViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Developer = game.Developer,
                ReleaseYear = game.ReleaseYear,
                Genre = game.Genre,
                ImageUrl = game.ImageUrl,
                IsInLibrary = userOwnedGameIds.Contains(game.Id),
                AverageRating = _context.Reviews.Where(r => r.GameId == game.Id).Average(r => (double?)r.Rating)
            }).ToList();
        var allGenres = _context.Games.Select(g => g.Genre).Distinct().ToList();
        
        var viewModel = new GameCatalogViewModel
        {
            Games = gamesListDto,
            AvailableGenres = allGenres
        };
        return View(viewModel);
    }
    [HttpGet]
    public IActionResult Create()
    {
        if (HttpContext.Session.GetString("UserRole") != "Admin")
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(GameCreateViewModel newGameDto)
    {
        if (HttpContext.Session.GetString("UserRole") != "Admin")
        {
            return RedirectToAction("Index", "Home");
        }
        
        if (ModelState.IsValid)
        {
            var gameEntity = new Game
            {
                Id = newGameDto.Id,
                Title = newGameDto.Title,
                Developer = newGameDto.Developer,
                ReleaseYear = newGameDto.ReleaseYear,
                Genre = newGameDto.Genre,
                ImageUrl = newGameDto.ImageUrl
            };
            _context.Games.Add(gameEntity);
            _context.SaveChanges(); 

            return RedirectToAction("Index");
        }

        return View(newGameDto);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        if (HttpContext.Session.GetString("UserRole") != "Admin")
        {
            return RedirectToAction("Index");
        }

        var game = _context.Games.Find(id);
        if (game == null)
        {
            return NotFound();
        }

        var viewModel = new GameEditViewModel
        {
            Id = game.Id,
            Title = game.Title,
            Developer = game.Developer,
            ReleaseYear = game.ReleaseYear,
            Genre = game.Genre,
            ImageUrl = game.ImageUrl
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Edit(int id, GameEditViewModel model)
    {
        if (HttpContext.Session.GetString("UserRole") != "Admin")
        {
            return RedirectToAction("Index");
        }

        if (id != model.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var gameFromDb = _context.Games.Find(id);
            if (gameFromDb == null) return NotFound();
            gameFromDb.Title = model.Title;
            gameFromDb.Developer = model.Developer;
            gameFromDb.ReleaseYear = model.ReleaseYear;
            gameFromDb.Genre = model.Genre;
            gameFromDb.ImageUrl = model.ImageUrl;

            _context.Games.Update(gameFromDb);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(model);
    }
}