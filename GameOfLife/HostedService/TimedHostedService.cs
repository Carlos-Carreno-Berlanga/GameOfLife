using GameOfLife.Dtos;
using GameOfLife.Hubs;
using GameOfLife.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.HostedService
{
    internal class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly IHubContext<GameNotifier> _hubContext;
        private readonly IGamestatusService _gamestatusService;
        private readonly IGameEvolutionService _gameEvolutionService;
        public TimedHostedService(ILogger<TimedHostedService> logger,
            IHubContext<GameNotifier> hubContext,
            IGamestatusService gamestatusService,
            IGameEvolutionService gameEvolutionService
            )
        {
            _logger = logger;
            _hubContext = hubContext;
            _gamestatusService = gamestatusService;
            _gameEvolutionService = gameEvolutionService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(async o => await DoWorkAsync(o), null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));
        }

        private async Task DoWorkAsync(object state)
        {
            GameStatusDto gameStatus = await _gamestatusService.GetGameStatusAsync();
            GameStatusDto nextGeneration = _gameEvolutionService.Evolve();
            await _hubContext.Clients.All.SendAsync("Notify", nextGeneration);
            _logger.LogInformation("Timed Background Service is working.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
