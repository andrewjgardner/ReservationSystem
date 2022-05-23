using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystemTests.Utilities
{
    public static class WebApplicationFactoryExtensions
    {
        public static WebApplicationFactory<T>
            WithManagerAuthentication<T>(this WebApplicationFactory<T> factory) where T : class
        {
            return factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication(TestAuthHandler.AuthenticationScheme)
                                           .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.AuthenticationScheme, options => { });

                });
            });
        }

        public static HttpClient
            CreateClientWithTestAuth<T>(this WebApplicationFactory<T> factory) where T : class
        {
            return factory.WithManagerAuthentication().CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        public static HttpClient
            CreateClientWithNoAuth<T>(this WebApplicationFactory<T> factory) where T : class
        {
            return factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
    }
}
