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
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IHubContext<GameNotifier> _hubContext;
        public readonly IServiceProvider _services;
        public TimedHostedService(ILogger<TimedHostedService> logger,
            IHubContext<GameNotifier> hubContext,
            IServiceProvider services
            )
        {
            _logger = logger;
            _hubContext = hubContext;
            _services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                _logger.LogInformation("Timed Background Service is starting.");
                await DoWorkAsync();
                await Task.Delay(1000); 
            }
        }

        public async Task DoWorkAsync()
        {
            using (var scope = _services.CreateScope())
            {
                IGamestatusService _gamestatusService = scope.ServiceProvider.GetRequiredService<IGamestatusService>();
                IGameEvolutionService _gameEvolutionService = scope.ServiceProvider.GetRequiredService<IGameEvolutionService>();
                GameStatusDto gameStatus = await _gamestatusService.GetGameStatusAsync();
                GameStatusDto nextGeneration = _gameEvolutionService.Evolve(gameStatus);
                await _gamestatusService.SetGameStatusAsync(nextGeneration);
                if (_hubContext?.Clients?.All != null)
                {
                    await _hubContext?.Clients?.All?.SendAsync("Notify", nextGeneration);
                }
                _logger.LogInformation("Timed Background Service is working.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
