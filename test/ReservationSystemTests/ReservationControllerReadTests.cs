using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Controllers;
using ReservationSystem.Models.Reservation;
using ReservationSystemTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReservationSystemTests
{
    public class ReservationControllerReadTests : IClassFixture<NullDatabaseFixture>
    {

        public ReservationControllerReadTests(NullDatabaseFixture fixture)
        {
            Fixture = fixture;
        }

        public NullDatabaseFixture Fixture { get; }

        [Fact]
        public async void Sitting_ReturnsAViewResult_WithAListOfSittings()
        {
            //Arrange
            using var context = Fixture.CreateContext();
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
