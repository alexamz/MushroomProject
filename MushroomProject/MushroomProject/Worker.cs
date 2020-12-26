using Microsoft.Extensions.Hosting;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MushroomProject
{
    public class Worker : BackgroundService
    {
        public IMessageHandler MessageHandler { get; }

        public Worker(IMessageHandler messageHandler)
        {
            MessageHandler = messageHandler;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await MessageHandler.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            MessageHandler.Stop(cancellationToken);
            return base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

    }
}
