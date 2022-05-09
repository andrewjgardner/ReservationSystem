using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Controllers;
using ReservationSystem.Data;
using ReservationSystem.Models.Reservation;
using ReservationSystem.Services;
using ReservationSystemTests.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReservationSystemTests.ReservationControllerTests
{
    public class ReadTests : IDisposable
    {
        private readonly DbConnection _connection;
        protected DbContextOptions<ApplicationDbContext> _contextOptions;
        private readonly ApplicationDbContext _context;
        private readonly PersonService _personService;

        public ReadTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .EnableSensitiveDataLogging()
                .Options;

            _context = CreateContext();
            _context.Database.EnsureCreated();

            _personService = new PersonService(_context);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public ApplicationDbContext CreateContext()
            => new ApplicationDbContext(_contextOptions, (modelBuilder) => new TestDataSeeder(modelBuilder));

        //[Fact]
        //public async void Sitting_ReturnsAViewResult_WithAListOfSittings()
        //{

        //    Arrange
        //    using var context = CreateContext();
        //    PostCreationSeeding.InitializeDbForRead(context);
        //    var controller = new ReservationController(context, _personService);

        //    Act
        //    var result = await controller.Sittings();

        //    Assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<IEnumerable<Sittings>>(
        //        viewResult.ViewData.Model);
        //    Assert.Equal(4, model.Count());
        //}

        //[Fact]
        //public async void ReservationForm_ReturnsSittingNotFoundWhenSittingIsNotFound()
        //{
        //    //Arrange
        //    int testSittingId = -1;
        //    using var context = CreateContext();
        //    PostCreationSeeding.InitializeDbForRead(context);
        //    var controller = new ReservationController(context, _personService);

        //    //Act
        //    var result = await controller.Create(testSittingId);

        //    //Assert
        //    var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
        //    Assert.Equal(testSittingId, notFoundObjectResult.Value);

        //} 
    }
}
