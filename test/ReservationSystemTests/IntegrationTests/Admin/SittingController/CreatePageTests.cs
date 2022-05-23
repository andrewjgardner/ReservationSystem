using ReservationSystem.Areas.Admin.Models.Sitting;
using ReservationSystemTests.Utilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace ReservationSystemTests.IntegrationTests.Admin.SittingController
{
    // https://gunnarpeipman.com/aspnet-core-integration-test-fake-user/ For more information on how I setup the authentication for testing

    public class CreatePageTests
        : IClassFixture<InMemoryWebApplicationFactory<Program>>
    {
        private readonly InMemoryWebApplicationFactory<Program> _factory;
        private const string _baseUrl = "/admin/sitting/create";

        public CreatePageTests(InMemoryWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_CreatePageRedirectsAnUnautheticatedUser()
        {
            //Arrange
            var client = _factory.CreateClientWithNoAuth();

            //Act
            var response = await client.GetAsync(_baseUrl);

            //Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("http://localhost/Identity/Account/Login", response.Headers.Location.ToString());
        }

        [Fact]
        public async Task Get_CreatePageIsReturnedForAuthenticatedUser()
        {
            //Arrange
            var client = _factory.CreateClientWithTestAuth();

            //Act
            var response = await client.GetAsync(_baseUrl);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [MemberData(nameof(GetValidFormData))]
        public async Task Post_CreateSittingAndRedirect(IEnumerable<KeyValuePair<string, string>> form)
        {
            //Arrange
            var client = _factory.CreateClientWithTestAuth();

            //Act
            var response = await client.PostAsync(_baseUrl, new FormUrlEncodedContent(form));

            //Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("/Admin/Sitting", response.Headers.Location.ToString());
        }

        [Fact]
        public async Task Post_EmptyFormData_ReturnsCreateViewWithErrors()
        {
            //Arrange
            var client = _factory.CreateClientWithTestAuth();
            var form = new List<KeyValuePair<string, string>>();

            //Act
            var response = await client.PostAsync(_baseUrl, new FormUrlEncodedContent(form));
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Contains("End Time must be set", responseString);
            Assert.Contains("Start Time must be set", responseString);
            Assert.Contains("Restaurant must be set", responseString);
            Assert.Contains("Sitting Type must be set", responseString);
        }

        [Theory]
        [MemberData(nameof(GetInvalidFormDataRecurring))]
        public async Task Post_RecurringSittingsDaily_InvalidFormReturnsCreateViewWithErrors(IEnumerable<KeyValuePair<string, string>> form)
        {
            //Arrange
            var client = _factory.CreateClientWithTestAuth();

            //Act
            var response = await client.PostAsync(_baseUrl, new FormUrlEncodedContent(form));

            //Assert
            response.EnsureSuccessStatusCode();
        }

        public static IEnumerable<object[]> GetValidFormData()
        {
            yield return new object[]
            {
                new[]
                {
                        new KeyValuePair<string, string>("Capacity", "100"),
                        new KeyValuePair<string, string>("StartTime", DateTime.Now.ToString()),
                        new KeyValuePair<string, string>("EndTime", DateTime.Now.AddHours(1).ToString()),
                        new KeyValuePair<string, string>("RestaurantId", "1"),
                        new KeyValuePair<string, string>("IsClosed","False"),
                        new KeyValuePair<string, string>("IsRecurring","False"),
                        new KeyValuePair<string, string>("Title", "Test Title"),
                        new KeyValuePair<string, string>("SittingTypeId", "1")
                }
            };

            yield return new object[]
            {
                new[]
                {
                        new KeyValuePair<string, string>("Capacity", "5"),
                        new KeyValuePair<string, string>("StartTime", DateTime.Now.ToString()),
                        new KeyValuePair<string, string>("EndTime", DateTime.Now.AddHours(1).ToString()),
                        new KeyValuePair<string, string>("RestaurantId", "1"),
                        new KeyValuePair<string, string>("IsClosed","True"),
                        new KeyValuePair<string, string>("IsRecurring","True"),
                        new KeyValuePair<string, string>("Title", "Test Title"),
                        new KeyValuePair<string, string>("SittingTypeId", "2"),
                        new KeyValuePair<string, string>("RecurringDays[0]","false"),
                        new KeyValuePair<string, string>("RecurringDays[1]","true"),
                        new KeyValuePair<string, string>("RecurringDays[2]","false"),
                        new KeyValuePair<string, string>("RecurringDays[3]","false"),
                        new KeyValuePair<string, string>("RecurringDays[4]","false"),
                        new KeyValuePair<string, string>("RecurringDays[5]","false"),
                        new KeyValuePair<string, string>("RecurringDays[6]","false"),
                        new KeyValuePair<string, string>("RecurringType","Daily"),
                        new KeyValuePair<string, string>("NumberToSchedule","12")

                }
            };
        }
        public static IEnumerable<object[]> GetInvalidFormData()
        {
            //StartTime after EndTime
            //Empty Title
            yield return new object[]
            {
                new[]
                {
                        new KeyValuePair<string, string>("Capacity", "5"),
                        new KeyValuePair<string, string>("EndTime", DateTime.Now.ToString()),
                        new KeyValuePair<string, string>("StartTime", DateTime.Now.AddHours(1).ToString()),
                        new KeyValuePair<string, string>("RestaurantId", "1"),
                        new KeyValuePair<string, string>("IsClosed","True"),
                        new KeyValuePair<string, string>("IsRecurring","True"),
                        new KeyValuePair<string, string>("Title", string.Empty),
                        new KeyValuePair<string, string>("SittingTypeId", "2"),
                        new KeyValuePair<string, string>("RecurringDays[0]","false"),
                        new KeyValuePair<string, string>("RecurringDays[1]","true"),
                        new KeyValuePair<string, string>("RecurringDays[2]","false"),
                        new KeyValuePair<string, string>("RecurringDays[3]","false"),
                        new KeyValuePair<string, string>("RecurringDays[4]","false"),
                        new KeyValuePair<string, string>("RecurringDays[5]","false"),
                        new KeyValuePair<string, string>("RecurringDays[6]","false"),
                        new KeyValuePair<string, string>("RecurringType","Daily"),
                        new KeyValuePair<string, string>("NumberToSchedule","12")

                }
            };
        }
        public static IEnumerable<object[]> GetInvalidFormDataRecurring()
        {
            //Missing Recurring Days
            yield return new object[]
            {
                new[]
                {
                        new KeyValuePair<string, string>("Capacity", "5"),
                        new KeyValuePair<string, string>("StartTime", DateTime.Now.ToString()),
                        new KeyValuePair<string, string>("EndTime", DateTime.Now.AddHours(1).ToString()),
                        new KeyValuePair<string, string>("RestaurantId", "1"),
                        new KeyValuePair<string, string>("IsClosed","True"),
                        new KeyValuePair<string, string>("IsRecurring","True"),
                        new KeyValuePair<string, string>("Title", "Test"),
                        new KeyValuePair<string, string>("SittingTypeId", "2"),
                        new KeyValuePair<string, string>("RecurringDays[0]","false"),
                        new KeyValuePair<string, string>("RecurringDays[6]","false"),
                        new KeyValuePair<string, string>("RecurringType","Daily"),
                        new KeyValuePair<string, string>("NumberToSchedule","12")
                }
            };

            //Null RecurringType
            yield return new object[]
            {
                new[]
                {
                        new KeyValuePair<string, string>("Capacity", "5"),
                        new KeyValuePair<string, string>("StartTime", DateTime.Now.ToString()),
                        new KeyValuePair<string, string>("EndTime", DateTime.Now.AddHours(1).ToString()),
                        new KeyValuePair<string, string>("RestaurantId", "1"),
                        new KeyValuePair<string, string>("IsClosed","True"),
                        new KeyValuePair<string, string>("IsRecurring","True"),
                        new KeyValuePair<string, string>("Title", "Test"),
                        new KeyValuePair<string, string>("SittingTypeId", "2"),
                        new KeyValuePair<string, string>("RecurringType", string.Empty),
                        new KeyValuePair<string, string>("NumberToSchedule","12")
                }
            };

            //Negative NumberToSchedule
            yield return new object[]
            {
                new[]
                {
                        new KeyValuePair<string, string>("Capacity", "5"),
                        new KeyValuePair<string, string>("StartTime", DateTime.Now.ToString()),
                        new KeyValuePair<string, string>("EndTime", DateTime.Now.AddHours(1).ToString()),
                        new KeyValuePair<string, string>("RestaurantId", "1"),
                        new KeyValuePair<string, string>("IsClosed","True"),
                        new KeyValuePair<string, string>("IsRecurring","True"),
                        new KeyValuePair<string, string>("Title", "Test"),
                        new KeyValuePair<string, string>("SittingTypeId", "2"),
                        new KeyValuePair<string, string>("RecurringType", "Daily"),
                        new KeyValuePair<string, string>("NumberToSchedule","-1")
                }
            };

            //Missing RecurringDays
            yield return new object[]
            {
                new[]
                {
                        new KeyValuePair<string, string>("Capacity", "5"),
                        new KeyValuePair<string, string>("StartTime", DateTime.Now.ToString()),
                        new KeyValuePair<string, string>("EndTime", DateTime.Now.AddHours(1).ToString()),
                        new KeyValuePair<string, string>("RestaurantId", "1"),
                        new KeyValuePair<string, string>("IsClosed","True"),
                        new KeyValuePair<string, string>("IsRecurring","True"),
                        new KeyValuePair<string, string>("Title", "Test"),
                        new KeyValuePair<string, string>("SittingTypeId", "2"),
                        new KeyValuePair<string, string>("RecurringType", "Weekly"),
                        new KeyValuePair<string, string>("NumberToSchedule","1")
                }
            };

            //All Recurring Days False
            yield return new object[]
            {
                new[]
                {
                        new KeyValuePair<string, string>("Capacity", "5"),
                        new KeyValuePair<string, string>("StartTime", DateTime.Now.ToString()),
                        new KeyValuePair<string, string>("EndTime", DateTime.Now.AddHours(1).ToString()),
                        new KeyValuePair<string, string>("RestaurantId", "1"),
                        new KeyValuePair<string, string>("IsClosed","True"),
                        new KeyValuePair<string, string>("IsRecurring","True"),
                        new KeyValuePair<string, string>("Title", "Test"),
                        new KeyValuePair<string, string>("SittingTypeId", "2"),
                        new KeyValuePair<string, string>("RecurringType", "Weekly"),
                        new KeyValuePair<string, string>("NumberToSchedule","1"),
                        new KeyValuePair<string, string>("Capacity", "5"),
                        new KeyValuePair<string, string>("EndTime", DateTime.Now.ToString()),
                        new KeyValuePair<string, string>("StartTime", DateTime.Now.AddHours(1).ToString()),
                        new KeyValuePair<string, string>("RestaurantId", "1"),
                        new KeyValuePair<string, string>("IsClosed","True"),
                        new KeyValuePair<string, string>("IsRecurring","True"),
                        new KeyValuePair<string, string>("Title", string.Empty),
                        new KeyValuePair<string, string>("SittingTypeId", "2"),
                        new KeyValuePair<string, string>("RecurringDays[0]","false"),
                        new KeyValuePair<string, string>("RecurringDays[1]","false"),
                        new KeyValuePair<string, string>("RecurringDays[2]","false"),
                        new KeyValuePair<string, string>("RecurringDays[3]","false"),
                        new KeyValuePair<string, string>("RecurringDays[4]","false"),
                        new KeyValuePair<string, string>("RecurringDays[5]","false"),
                        new KeyValuePair<string, string>("RecurringDays[6]","false"),
                        new KeyValuePair<string, string>("RecurringType","Daily"),
                        new KeyValuePair<string, string>("NumberToSchedule","12")
                }
            };

            //Example test failure
            //yield return new object[]
            //{
            //    new[]
            //    {
            //            new KeyValuePair<string, string>("Capacity", "5"),
            //            new KeyValuePair<string, string>("StartTime", DateTime.Now.ToString()),
            //            new KeyValuePair<string, string>("EndTime", DateTime.Now.AddHours(1).ToString()),
            //            new KeyValuePair<string, string>("RestaurantId", "1"),
            //            new KeyValuePair<string, string>("IsClosed","True"),
            //            new KeyValuePair<string, string>("IsRecurring","True"),
            //            new KeyValuePair<string, string>("Title", "Test Title"),
            //            new KeyValuePair<string, string>("SittingTypeId", "2"),
            //            new KeyValuePair<string, string>("RecurringDays[0]","false"),
            //            new KeyValuePair<string, string>("RecurringDays[1]","true"),
            //            new KeyValuePair<string, string>("RecurringDays[2]","false"),
            //            new KeyValuePair<string, string>("RecurringDays[3]","false"),
            //            new KeyValuePair<string, string>("RecurringDays[4]","false"),
            //            new KeyValuePair<string, string>("RecurringDays[5]","false"),
            //            new KeyValuePair<string, string>("RecurringDays[6]","false"),
            //            new KeyValuePair<string, string>("RecurringType","Daily"),
            //            new KeyValuePair<string, string>("NumberToSchedule","12")

            //    }
            //};
        }
    }
}









