using ReservationSystem.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace ReservationSystemTests;

public class ReservationControllerTests
{
    [Fact]
    public void Sittings_ReturnsAViewResult_WithAListOfSittings()
    {
        //Arrange
        var mockSittings = GetTestSittings();
        

        //Act

    }


    private List<Sitting> GetTestSittings()
    {
        var sittings = new List<Sitting>();
        sittings.Add(new Sitting
        {
            Id = 1,
            Title = "Test One",
            StartTime = new DateTime(2022, 05, 20, 8, 30, 00),
            EndTime = new DateTime(2022, 05, 20, 12, 00, 0),
            Capacity = 100,
            ResDuration = 45
        });
        sittings.Add(new Sitting
        {
            Id = 2,
            Title = "Test Two",
            StartTime = new DateTime(2022, 05, 20, 13, 30, 00),
            EndTime = new DateTime(2022, 05, 20, 16, 00, 0),
            Capacity = 100,
            ResDuration = 60
        });
        sittings.Add(new Sitting
        {
            Id = 3,
            Title = "Test Three",
            StartTime = new DateTime(2022, 05, 20, 18, 00, 00),
            EndTime = new DateTime(2022, 05, 20, 22, 00, 00),
            Capacity = 110,
            ResDuration = 90
        });
        return sittings;
    }
}