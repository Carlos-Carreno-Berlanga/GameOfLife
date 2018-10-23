using GameOfLife.Dtos;
using GameOfLife.Services.Interfaces;

namespace GameOfLife.Services.Implementation
{
    public class GameEvolutionService : IGameEvolutionService
    {
        public GameStatusDto Evolve(GameStatusDto currentGameStatus)
        {
            //throw new NotImplementedException();
            int nextGeneration = currentGameStatus.Generation + 1;
            string[,] nextBoard = (string[,])currentGameStatus.Board.Clone();
            int cols = currentGameStatus.Board.GetLength(0);
            int rows = currentGameStatus.Board.GetLength(1);
            for (int i = 0; i < currentGameStatus.Board.GetLength(0); i++)
            {
                for (int j = 0; j < currentGameStatus.Board.GetLength(1); j++)
                {
                    int count = 0;
                    if (i > 0) if (!string.IsNullOrEmpty(currentGameStatus.Board[i - 1, j])) count++;
                    if (i > 0 && j > 0) if (!string.IsNullOrEmpty(currentGameStatus.Board[i - 1, j - 1])) count++;
                    if (i > 0 && j < rows - 1) if (!string.IsNullOrEmpty(currentGameStatus.Board[i - 1, j + 1])) count++;
                    if (j < rows - 1) if (!string.IsNullOrEmpty(currentGameStatus.Board[i, j + 1])) count++;
                    if (j > 0) if (!string.IsNullOrEmpty(currentGameStatus.Board[i, j - 1])) count++;
                    if (i < cols - 1) if (!string.IsNullOrEmpty(currentGameStatus.Board[i + 1, j])) count++;
                    if (i < cols - 1 && j > 0) if (!string.IsNullOrEmpty(currentGameStatus.Board[i + 1, j - 1])) count++;
                    if (i < cols - 1 && j < rows - 1) if (!string.IsNullOrEmpty(currentGameStatus.Board[i + 1, j + 1])) count++;
                    if (!string.IsNullOrEmpty(currentGameStatus.Board[i, j]) && (count < 2 || count > 3)) nextBoard[i, j] = null;
                    if (string.IsNullOrEmpty(currentGameStatus.Board[i, j]) && count == 3) nextBoard[i, j] = "#47af22";
                }
            }
            GameStatusDto nextGameStatus = new GameStatusDto(currentGameStatus.Columns, currentGameStatus.Rows, nextBoard, nextGeneration);
            return nextGameStatus;
        }

    }
}
