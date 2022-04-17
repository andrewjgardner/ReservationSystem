using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Controllers;
using ReservationSystem.Data;
using ReservationSystem.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ReservationSystemTests;

public class ReservationControllerTests : IClassFixture<TestDatabaseFixture>
{

    public ReservationControllerTests(TestDatabaseFixture fixture)
    {
        Fixture = fixture;
    }

    public TestDatabaseFixture Fixture { get; }

    [Fact]
    public async void GetSitting()
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
        Assert.Equal(3, model.Count());
    }


}