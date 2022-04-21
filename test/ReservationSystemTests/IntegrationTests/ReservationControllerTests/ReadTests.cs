using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Controllers;
using ReservationSystem.Data;
using ReservationSystem.Models.Reservation;
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
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public ApplicationDbContext CreateContext()
            => new ApplicationDbContext(_contextOptions, (modelBuilder) => new TestDataSeeder(modelBuilder));

        [Fact]
        public async void Sitting_ReturnsAViewResult_WithAListOfSittings()
        {

            //Arrange
            using var context = CreateContext();
            PostCreationSeeding.InitializeDbForRead(context);
            var controller = new ReservationController(context);

            //Act
            var result = await controller.Sittings();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<SittingsVM>>(
                viewResult.ViewData.Model);
            Assert.Equal(4, model.Count());
        }
    }
}
