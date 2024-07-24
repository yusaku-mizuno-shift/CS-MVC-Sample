using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Controllers;
using MvcMovie.Data;
using MvcMovie.Models;
using System.Globalization;
using Xunit;

namespace MvcMovie.Tests
{
    public class HomeControllerTests
    {
        private readonly MvcMovieContext _context;

        public HomeControllerTests()
        {
            string? env = Environment.GetEnvironmentVariable("sqlOptions");
            if (env == null)
            {
                this._context = new MvcMovieContext(new DbContextOptionsBuilder<MvcMovieContext>().UseSqlite("Data Source=MvcMovie.db").Options);
            }
            else
            {
                this._context = new MvcMovieContext(new DbContextOptionsBuilder<MvcMovieContext>().UseSqlServer(env).Options);
            }
        }

        [Fact]
        public void Index_ReturnsAViewResult()
        {
            this._context.Database.EnsureCreated();

            // Arrange
            var controller = new HomeController();
            string Title = "TEST MOVIE 1";
            var addMovie = new Movie
            {
                Title = "TEST MOVIE 1",
                ReleaseDate = DateTime.Parse("1989-2-12", new CultureInfo("en-US")),
                Genre = "Romantic Comedy",
                Price = 7.99M,
                Rating = "R"
            };
            this._context.Movie.Add(addMovie);
            this._context.SaveChanges();


            // Act
            var result = controller.Index();
            var movieResult = this._context.Movie.SingleOrDefault(c => c.Title == Title) ?? new Movie();

            // Assert
            Assert.IsType<ViewResult>(result);
            Assert.Equal("TEST MOVIE 1", movieResult.Title);
            Assert.Equal(DateTime.Parse("1989-2-12", new CultureInfo("en-US")), movieResult.ReleaseDate);
            Assert.Equal("Romantic Comedy", movieResult.Genre);
            Assert.Equal(7.99M, movieResult.Price);
            Assert.Equal("R", movieResult.Rating);

            this._context.Database.EnsureDeleted();
        }
    }
}