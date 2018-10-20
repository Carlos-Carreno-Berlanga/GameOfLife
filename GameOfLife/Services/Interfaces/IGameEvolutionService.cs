using GameOfLife.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife.Services.Interfaces
{
    public interface IGameEvolutionService
    {
        GameStatusDto Evolve();
        
    }
}
