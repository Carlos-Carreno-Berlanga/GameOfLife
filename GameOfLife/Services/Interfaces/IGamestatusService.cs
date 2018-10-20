using GameOfLife.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife.Services.Interfaces
{
    public interface IGamestatusService
    {
        Task<GameStatusDto> GetGameStatusAsync();
        Task SetGameStatusAsync(GameStatusDto gameStatus);
    }
}
