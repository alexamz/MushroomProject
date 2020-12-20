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
            MessageHandler.Start();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
