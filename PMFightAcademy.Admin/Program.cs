using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PMFightAcademy.Admin.DataBase;

namespace PMFightAcademy.Admin
{
#pragma warning disable 1591
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(collection => collection.AddHostedService<TimerSlotService>())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    /// <summary>
    /// Service for the updating outdated slots in db
    /// </summary>
    public class TimerSlotService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public TimerSlotService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                var tm = new TimerCallback(UpdateSlotsTable);
                var timer = new Timer(tm, null, TimeSpan.FromSeconds(0), TimeSpan.FromHours(1));
                while (true) { }
                timer.Dispose();
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async void UpdateSlotsTable(object obj)
        {
            using var scope = _serviceProvider.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetRequiredService<AdminContext>();
            var slots = await dbContext.Slots.Where(x => x.Expired == false).ToListAsync();

            foreach (var slot in slots)
            {
                var slotStart = slot.Date.Add(slot.StartTime);
                if (slotStart < DateTime.Now)
                {
                    slot.Expired = true;
                    dbContext.Slots.Update(slot);
                }
            }

            await dbContext.SaveChangesAsync();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Table \"SLOTS\" was updated!");
            Console.ResetColor();
        }
    }
#pragma warning restore 1591
}
