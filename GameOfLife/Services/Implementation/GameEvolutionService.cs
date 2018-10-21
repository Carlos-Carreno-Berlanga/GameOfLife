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
            bool[,] nextBoard = (bool[,])currentGameStatus.Board.Clone();
            int cols = currentGameStatus.Board.GetLength(0);
            int rows = currentGameStatus.Board.GetLength(1);
            for (int i = 0; i < currentGameStatus.Board.GetLength(0); i++)
            {
                for (int j = 0; j < currentGameStatus.Board.GetLength(1); j++)
                {
                    int count = 0;
                    if (i > 0) if (currentGameStatus.Board[i - 1, j]) count++;
                    if (i > 0 && j > 0) if (currentGameStatus.Board[i - 1, j - 1]) count++;
                    if (i > 0 && j < cols - 1) if (currentGameStatus.Board[i - 1, j + 1]) count++;
                    if (j < cols - 1) if (currentGameStatus.Board[i, j + 1]) count++;
                    if (j > 0) if (currentGameStatus.Board[i, j - 1]) count++;
                    if (i < rows - 1) if (currentGameStatus.Board[i + 1, j]) count++;
                    if (i < rows - 1 && j > 0) if (currentGameStatus.Board[i + 1, j - 1]) count++;
                    if (i < rows - 1 && j < cols - 1) if (currentGameStatus.Board[i + 1, j + 1]) count++;
                    if (currentGameStatus.Board[i, j] && (count < 2 || count > 3)) nextBoard[i, j] = false;
                    if (!currentGameStatus.Board[i, j] && count == 3) nextBoard[i, j] = true;
                }
            }
            GameStatusDto nextGameStatus = new GameStatusDto(currentGameStatus.Columns, currentGameStatus.Rows, nextBoard, nextGeneration);
            return nextGameStatus;
        }

    }
}
