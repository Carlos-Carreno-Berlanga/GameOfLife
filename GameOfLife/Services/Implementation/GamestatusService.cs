using GameOfLife.Dtos;
using GameOfLife.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using static GameOfLife.Constants.AppConstants;

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

        private bool getBooleanRandom()
        {
            Random gen = new Random();
            int prob = gen.Next(100);
            return prob <= 50;
        }

        public async Task<GameStatusDto> GetGameStatusAsync()
        {
            await _distributedCache.GetStringAsync(cacheKey);
            string gameStatusString = await _distributedCache.GetStringAsync(cacheKey);
            if (string.IsNullOrEmpty(gameStatusString))
            {
                string[,] randomBoard = new string[boardColumns, boardRows];
                for(int i = 0; i < randomBoard.GetLength(0); i++)
                {
                    for(int j=0; j<randomBoard.GetLength(1);j++)
                    {
                        if (getBooleanRandom())
                        {
                            randomBoard[i, j] = defaultCellColor;
                        }
                        else
                        {
                            randomBoard[i, j] = null;
                        }

                    }
                }
               GameStatusDto initialGame = new GameStatusDto(boardColumns, boardRows, randomBoard, 0);
                await SetGameStatusAsync(initialGame);
                return initialGame;
            }

            GameStatusDto gameStatus = JsonConvert.DeserializeObject<GameStatusDto>(gameStatusString);
            return gameStatus;

        }

        public async Task SetGameStatusAsync(GameStatusDto gameStatus)
        {
            await _distributedCache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(gameStatus));
        }
    }
}
