using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PMFightAcademy.Admin.DataBase;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PMFightAcademy.Admin
{
    /// <summary>
    /// Service for the updating outdated slots in db
    /// </summary>
    public class TimerSlotService : IHostedService
    {
#pragma warning disable 1591
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
                while (true) { new AutoResetEvent(false).WaitOne(TimeSpan.FromHours(1), true); }
                timer.Dispose();
            }, CancellationToken.None);

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
            var saving = true;
            try
            {
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
            }
            catch
            {
                saving = false;
            }
            finally
            {
                if (saving)
                    await dbContext.SaveChangesAsync();
            }
        }
    }
#pragma warning restore 1591
}