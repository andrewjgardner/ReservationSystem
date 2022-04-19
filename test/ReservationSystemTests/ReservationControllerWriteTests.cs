using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Controllers;
using ReservationSystem.Data;
using ReservationSystem.Models.Reservation;
using ReservationSystemTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ReservationSystemTests;

public class ReservationControllerWriteTests
{

    //public ReservationControllerWriteTests(WriteDatabaseFixture fixture)
    //{
    //    Fixture = fixture;
    //}

    //public WriteDatabaseFixture Fixture { get; }


    //[Fact]
    //public async void Sitting_ReturnsAViewResult_IfSittingsIsEmpty()
    //{
    //    //Arrange
    //    using var context = Fixture.CreateContext();
    //    var controller = new ReservationController(context);

    //    //Act
    //    var result = await controller.Sittings();

    //    //Assert
    //    var viewResult = Assert.IsType<ViewResult>(result);
    //    var model = Assert.IsAssignableFrom<IEnumerable<SittingsVM>>(
    //        viewResult.ViewData.Model);
    //    Assert.Null(model);
    //}

}