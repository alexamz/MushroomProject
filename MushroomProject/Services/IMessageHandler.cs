using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IMessageHandler
    {
        Task StartAsync(CancellationToken stoppingToken);
        void Stop(CancellationToken stoppingToken);
    }
}