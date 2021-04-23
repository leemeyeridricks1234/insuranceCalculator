using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Insurance.Tests.IntegrationTests
{
    public class ControllerTestFixture : IDisposable
    {
        private readonly IHost _host;
        public const string BaseUrl = "http://localhost:5011";

        public ControllerTestFixture()
        {
            _host = new HostBuilder()
                   .ConfigureWebHostDefaults(
                        b => b.UseUrls(BaseUrl)
                              .UseStartup<ControllerTestStartup>()
                    )
                   .Build();

            _host.Start();
        }

        public void Dispose() => _host.Dispose();
    }
}
