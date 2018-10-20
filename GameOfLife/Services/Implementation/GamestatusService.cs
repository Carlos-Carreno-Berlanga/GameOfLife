using GameOfLife.Dtos;
using GameOfLife.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife.Services.Implementation
{
    public class GamestatusService : IGamestatusService
    {
        private readonly IDistributedCache _distributedCache;
        const string cacheKey = "gameStatus";
        public GamestatusService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<GameStatusDto> GetGameStatusAsync()
        {
            await _distributedCache.GetStringAsync(cacheKey);
            string gameStatusString = await _distributedCache.GetStringAsync(cacheKey);
            if (string.IsNullOrEmpty(gameStatusString))
            {
                await SetGameStatusAsync(new GameStatusDto(1, 2));
            }
            else
            {
                JsonConvert.DeserializeObject<GameStatusDto>(gameStatusString);
            }
            return new GameStatusDto(1, 2);
        }

        public async Task SetGameStatusAsync(GameStatusDto gameStatus)
        {

            await _distributedCache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(gameStatus));
        }
    }
}
