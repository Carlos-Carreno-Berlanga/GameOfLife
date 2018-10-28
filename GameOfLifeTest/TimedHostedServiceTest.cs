using GameOfLife.Dtos;
using GameOfLife.HostedService;
using GameOfLife.Hubs;
using GameOfLife.Services.Implementation;
using GameOfLife.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GameOfLifeTest
{

    public class TimedHostedServiceTest
    {

        private readonly Mock<IHubContext<GameNotifier>> _hubContextMock = new Mock<IHubContext<GameNotifier>>();
        private readonly Mock<ILogger<TimedHostedService>> _loggerMock = new Mock<ILogger<TimedHostedService>>();
        private readonly Mock<IServiceProvider> _serviceProviderMock = new Mock<IServiceProvider>();
        private readonly Mock<IDistributedCache> distributedCacheMock = new Mock<IDistributedCache>();
        private readonly Mock<IGameEvolutionService> _gameEvolutionService = new Mock<IGameEvolutionService>();
        private readonly Mock<IGamestatusService> _gamestatusServiceMock = new Mock<IGamestatusService>();
        private readonly Mock<ILifeformFactory> _lifeformFactoryMock = new Mock<ILifeformFactory>();
        private TimedHostedService _timedHostedService;


        [Fact]
        public async Task DoWorkAsync_Gets_Game_Status_Then_Evolve_And_Then_Set_New_Game_Status()
        {
            //assert

            _serviceProviderMock
                .Setup(x => x.GetService(typeof(IGamestatusService)))
                .Returns(_gamestatusServiceMock.Object);

            _serviceProviderMock
                .Setup(x => x.GetService(typeof(IGameEvolutionService)))
                .Returns(_gameEvolutionService.Object);
            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(_serviceProviderMock.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            _serviceProviderMock
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);

            _timedHostedService = new TimedHostedService(
                _loggerMock.Object,
                _hubContextMock.Object,
                _serviceProviderMock.Object
                );

            await _timedHostedService.DoWorkAsync();
            _gamestatusServiceMock.Verify(gamestatusService => gamestatusService.GetGameStatusAsync(), Times.Once);
            _gameEvolutionService.Verify(gameEvolutionService => gameEvolutionService.Evolve(It.IsAny<GameStatusDto>()), Times.Once);
            _gamestatusServiceMock.Verify(gamestatusService => gamestatusService.SetGameStatusAsync(It.IsAny<GameStatusDto>()), Times.Once);
        }
    }
}
