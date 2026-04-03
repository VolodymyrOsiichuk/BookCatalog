using BookCatalog.Models.Entitites;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();

        if (!await context.Genres.AnyAsync())
        {
            var genres = new List<Genre>
            {
                new()
                {
                    Name = "Programming",
                    Description = "Books about software development and coding."
                },
                new()
                {
                    Name = "Science Fiction",
                    Description = "Fictional books based on futuristic science and technology."
                },
                new()
                {
                    Name = "Business",
                    Description = "Books about entrepreneurship, marketing and management."
                }
            };

            context.Genres.AddRange(genres);
            await context.SaveChangesAsync();
        }

        if (!await context.Authors.AnyAsync())
        {
            var authors = new List<Author>
            {
                new()
                {
                    FullName = "Robert C. Martin",
                    Biography = "Author of Clean Code and expert in software craftsmanship.",
                    Email = "unclebob@example.com"
                },
                new()
                {
                    FullName = "Isaac Asimov",
                    Biography = "Famous science fiction writer and professor of biochemistry.",
                    Email = "asimov@example.com"
                },
                new()
                {
                    FullName = "Simon Sinek",
                    Biography = "Motivational speaker and author of leadership books.",
                    Email = "sinek@example.com"
                }
            };

            context.Authors.AddRange(authors);
            await context.SaveChangesAsync();
        }

        if (!await context.Users.AnyAsync())
        {
            var users = new List<User>
            {
                new()
                {
                    Username = "admin",
                    Email = "admin@bookcatalog.com",
                    PasswordHash = HashPassword("Admin123!"),
                    Role = "Admin"
                },
                new()
                {
                    Username = "user1",
                    Email = "user1@bookcatalog.com",
                    PasswordHash = HashPassword("User123!"),
                    Role = "User"
                },
                new()
                {
                    Username = "user2",
                    Email = "user2@bookcatalog.com",
                    PasswordHash = HashPassword("User123!"),
                    Role = "User"
                }
            };

            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }

        if (!await context.Books.AnyAsync())
        {
            var programming = await context.Genres.FirstAsync(x => x.Name == "Programming");
            var sciFi = await context.Genres.FirstAsync(x => x.Name == "Science Fiction");
            var business = await context.Genres.FirstAsync(x => x.Name == "Business");

            var uncleBob = await context.Authors.FirstAsync(x => x.Email == "unclebob@example.com");
            var asimov = await context.Authors.FirstAsync(x => x.Email == "asimov@example.com");
            var sinek = await context.Authors.FirstAsync(x => x.Email == "sinek@example.com");

            var books = new List<Book>
            {
                new()
                {
                    Title = "Clean Code",
                    Description = "A handbook of agile software craftsmanship.",
                    PublishedYear = 2008,
                    Price = 39.99m,
                    GenreId = programming.Id,
                    AuthorId = uncleBob.Id
                },
                new()
                {
                    Title = "I, Robot",
                    Description = "A collection of short stories about robots and ethics.",
                    PublishedYear = 1950,
                    Price = 19.99m,
                    GenreId = sciFi.Id,
                    AuthorId = asimov.Id
                },
                new()
                {
                    Title = "Start With Why",
                    Description = "A book about leadership and inspiration.",
                    PublishedYear = 2009,
                    Price = 24.99m,
                    GenreId = business.Id,
                    AuthorId = sinek.Id
                }
            };

            context.Books.AddRange(books);
            await context.SaveChangesAsync();
        }

        if (!await context.Comments.AnyAsync())
        {
            var book1 = await context.Books.FirstAsync();
            var user1 = await context.Users.FirstAsync(x => x.Username == "user1");
            var user2 = await context.Users.FirstAsync(x => x.Username == "user2");

            var comments = new List<Comment>
            {
                new()
                {
                    Content = "Great book for developers!",
                    BookId = book1.Id,
                    UserId = user1.Id,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Content = "Very inspiring and useful.",
                    BookId = book1.Id,
                    UserId = user2.Id,
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.Comments.AddRange(comments);
            await context.SaveChangesAsync();
        }
    }

    private static string HashPassword(string password)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}