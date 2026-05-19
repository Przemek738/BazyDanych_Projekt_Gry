using Microsoft.AspNetCore.Mvc;
using BacklogManager.Data;
using BacklogManager.Models;
using BacklogManager.DTOs;

namespace BacklogManager.Controllers;
public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create(int gameId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            var game = _context.Games.Find(gameId);
            if (game == null) return NotFound();
            
            var existingReview = _context.Reviews.FirstOrDefault(r => r.GameId == gameId && r.UserId == userId);

            var viewModel = new ReviewCreateViewModel
            {
                GameId = game.Id,
                GameTitle = game.Title,
                GameImageUrl = game.ImageUrl
            };
            
            if (existingReview != null)
            {
                viewModel.Rating = existingReview.Rating;
                viewModel.Comment = existingReview.Comment;
                
                ViewData["CardTitle"] = "Zmień swoją ocenę";
            }
            else
            {
                ViewData["CardTitle"] = "Oceń grę";
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(ReviewCreateViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            if (model.Rating < 1 || model.Rating > 10)
            {
                ModelState.AddModelError("Rating", "Ocena musi być w przedziale 1-10.");
                return View(model);
            }
            
            var existingReview = _context.Reviews.FirstOrDefault(r => r.GameId == model.GameId && r.UserId == userId);

            if (existingReview != null)
            {
                existingReview.Rating = model.Rating;
                existingReview.Comment = model.Comment;
                existingReview.CreatedAt = DateTime.Now;

                _context.Reviews.Update(existingReview);
                TempData["SuccessMessage"] = "Twoja ocena została pomyślnie zaktualizowana!";
            }
            else
            {
                var newReview = new Review
                {
                    UserId = (int)userId,
                    GameId = model.GameId,
                    Rating = model.Rating,
                    Comment = model.Comment
                };

                _context.Reviews.Add(newReview);
                TempData["SuccessMessage"] = "Twoja recenzja została opublikowana!";
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "UserGames");
        }
    }