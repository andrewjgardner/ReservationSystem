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
using Xunit;

namespace ReservationSystemTests.ReservationControllerTests
{
    public class WriteTests : IDisposable
    {
        private readonly DbConnection _connection;
        protected DbContextOptions<ApplicationDbContext> _contextOptions;

        public WriteTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;

            using var context = CreateContext();
            context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public ApplicationDbContext CreateContext()
            => new ApplicationDbContext(_contextOptions, (modelBuilder) => new TestDataSeeder(modelBuilder));

        [Fact]
        public async void Sitting_ReturnsAViewResult_IfSittingsIsEmpty()
        {
            //Arrange
            using var context = CreateContext();
            var controller = new ReservationController(context);

            //Act
            var result = await controller.Sittings();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<SittingsVM>>(
                viewResult.ViewData.Model);

            Assert.Equal(new List<SittingsVM>(), model);
        }

    }
}