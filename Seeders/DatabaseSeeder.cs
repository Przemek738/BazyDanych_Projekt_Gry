using Microsoft.EntityFrameworkCore;
using BacklogManager.Data;
using BacklogManager.Models; 

namespace BacklogManager.Seeders
{
    public static class DatabaseSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Platforms.Any())
            {
                context.Platforms.AddRange(
                    new Platform { Name = "PC" },
                    new Platform { Name = "PlayStation 5" },
                    new Platform { Name = "PlayStation 4" },
                    new Platform { Name = "Xbox Series X/S" },
                    new Platform { Name = "Nintendo Switch" }
                );
                context.SaveChanges();
            }
            
            if (!context.Users.Any(u => u.Username == "admin"))
            {
                context.Users.Add(new User
                {
                    Username = "admin",
                    Email = "admin@backlog.pl",
                    Password = PasswordHasher.HashPassword("admin123"),
                    Role = "Admin"
                });
                context.SaveChanges();
            }
            
            if (!context.Games.Any())
            {
                context.Games.AddRange(
                    new Game 
                    { 
                        Title = "The Witcher 3: Wild Hunt", 
                        Developer = "CD Projekt RED", 
                        ReleaseYear = 2015, 
                        Genre = "RPG", 
                        ImageUrl = "/Images/placeholder.png" 
                    },
                    new Game 
                    { 
                        Title = "Elden Ring", 
                        Developer = "FromSoftware", 
                        ReleaseYear = 2022, 
                        Genre = "Action RPG", 
                        ImageUrl = "/Images/placeholder.png" 
                    },
                    new Game 
                    { 
                        Title = "Cyberpunk 2077", 
                        Developer = "CD Projekt RED", 
                        ReleaseYear = 2020, 
                        Genre = "RPG", 
                        ImageUrl = "/Images/placeholder.png" 
                    },
                    new Game 
                    { 
                        Title = "Stardew Valley", 
                        Developer = "ConcernedApe", 
                        ReleaseYear = 2016, 
                        Genre = "Symulator", 
                        ImageUrl = "/Images/placeholder.png" 
                    }
                );
                context.SaveChanges();
            }
        }
    }
}