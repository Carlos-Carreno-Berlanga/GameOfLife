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
        public GameStatusDto Evolve(GameStatusDto currentGameStatus)
        {
            //throw new NotImplementedException();
            int nextGeneration = currentGameStatus.Generation + 1;
            //bool[,] nextBoard = new bool[currentGameStatus.Columns, currentGameStatus.Rows];
            GameStatusDto nextGameStatus= new GameStatusDto(currentGameStatus.Columns, currentGameStatus.Rows, currentGameStatus.Board, nextGeneration);
            return nextGameStatus;
        }
    }
}
