using Application.Interfaces;
using Azul.Framework.Api;
using Infrastructure.Ioc;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public sealed class Program : BaseProgram
    {
        private async static Task Main()
        {
            IHost host = null;
            var container = new Container();
            try
            {
                host = new HostBuilder()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureServices((hostContext, services) =>
                    {
                        _ = new Bootstrapper(services, container);
                    }).Start();
            }
            catch (Exception ex)
            {
              Console.WriteLine($"Error injecting services. Exception: {ex}");
            }
            finally
            {
                await StartApplication(container);
                await StopHost(host);
            }
        }

        private static async Task StopHost(IHost host)
        {   
            if (host != null)
            {
                await host.StopAsync();
                host.Dispose();
            }
        }

        private async static Task StartApplication(Container container)
        {   
           await container.GetInstance<IPartNumberApp>().StartAsync(CancellationToken.None);
           await container.GetInstance<IPartNumberQuantityApp>().StartAsync(CancellationToken.None);
        }
    }
}