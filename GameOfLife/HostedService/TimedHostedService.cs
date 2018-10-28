using GameOfLife.Dtos;
using GameOfLife.Hubs;
using GameOfLife.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.HostedService
{
    internal class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly IHubContext<GameNotifier> _hubContext;
        private readonly IServiceScopeFactory _scopeFactory;
        public TimedHostedService(ILogger<TimedHostedService> logger,
            IHubContext<GameNotifier> hubContext,
            IServiceScopeFactory scopeFactory
            )
        {
            _logger = logger;
            _hubContext = hubContext;
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(async o => await DoWorkAsync(o), null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10));
        }

        private async Task DoWorkAsync(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                IGamestatusService _gamestatusService = scope.ServiceProvider.GetRequiredService<IGamestatusService>();
                IGameEvolutionService _gameEvolutionService = scope.ServiceProvider.GetRequiredService<IGameEvolutionService>();
                GameStatusDto gameStatus = await _gamestatusService.GetGameStatusAsync();
                GameStatusDto nextGeneration = _gameEvolutionService.Evolve(gameStatus);
                await _gamestatusService.SetGameStatusAsync(nextGeneration);
                await _hubContext.Clients.All.SendAsync("Notify", nextGeneration);
                _logger.LogInformation("Timed Background Service is working.");
            }
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
