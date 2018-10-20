using GameOfLife.Dtos;
using GameOfLife.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife.Services.Implementation
{
    public class GameEvolutionService : IGameEvolutionService
    {
        public GameStatusDto Evolve()
        {
            //throw new NotImplementedException();
            return new GameStatusDto();
        }
    }
}
