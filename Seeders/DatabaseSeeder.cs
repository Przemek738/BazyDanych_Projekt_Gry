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
                    new Platform { Name = "Nintendo Switch" },
                    new Platform { Name = "Mobile" },
                    new Platform { Name = "Steamdeck"}
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
                        ImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/coaarl.webp" 
                    },
                    new Game 
                    { 
                        Title = "Elden Ring", 
                        Developer = "FromSoftware", 
                        ReleaseYear = 2022, 
                        Genre = "Action RPG", 
                        ImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co4jni.webp" 
                    },
                    new Game 
                    { 
                        Title = "Cyberpunk 2077", 
                        Developer = "CD Projekt RED", 
                        ReleaseYear = 2020, 
                        Genre = "RPG", 
                        ImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/coaih8.webp" 
                    },
                    new Game 
                    { 
                        Title = "Stardew Valley", 
                        Developer = "ConcernedApe", 
                        ReleaseYear = 2016, 
                        Genre = "Symulator", 
                        ImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/coa93h.webp" 
                    },
                    new Game
                    {
                        Title = "Hyperdimension Neptunia Re;Birth1",
                        Developer = "Compile Heart",
                        ReleaseYear = 2013,
                        Genre = "JRPG",
                        ImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1sz1.webp"
                    },
                    new Game
                    {
                        Title = "Hyperdimension Neptunia Re;Birth2",
                        Developer = "Compile Heart",
                        ReleaseYear = 2014,
                        Genre = "JRPG",
                        ImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1sz2.webp"
                    },
                	new Game
                    {
                        Title = "Factorio",
                        Developer = "Wube Software",
                        ReleaseYear = 2020,
                        Genre = "Simulator",
                        ImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1tfy.webp"
                    }, 
                    new Game
                    {
                    Title = "Persona 5 Royal",
                    Developer = "Atlus",
                    ReleaseYear = 2019,
                    Genre = "JRPG",
                    ImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/cobaqh.webp"
                    },
                    new Game
                    {
                        Title = "Hollow Knight: Silksong",
                        Developer = "Team Cherry",
                        ReleaseYear = 2025,
                        Genre = "Metroidvania",
                        ImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/cobebu.webp"
                    },
                    new Game
                    {
                        Title = "Red Dead Redemption 2",
                        Developer = "Rockstar Games",
                        ReleaseYear = 2018,
                        Genre = "Action RPG",
                        ImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1q1f.webp"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}