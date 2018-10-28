using GameOfLife.Dtos;
using GameOfLife.ResourceObjects;
using GameOfLife.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife.Services.Implementation
{
    public class GameEvolutionService : IGameEvolutionService
    {
        private readonly IGamestatusService _gamestatusService;
        private readonly ILifeformFactory _lifeformFactory;
        public GameEvolutionService(IGamestatusService gamestatusService,
            ILifeformFactory lifeformFactory)
        {
            _gamestatusService = gamestatusService;
            _lifeformFactory = lifeformFactory;
        }

        private void increaseColorCounter(SortedDictionary<string, int> colorCounterDictionary, string color)
        {
            if (!colorCounterDictionary.ContainsKey(color))
            {
                colorCounterDictionary.Add(color, 1);
            }
            else
            {
                colorCounterDictionary[color]++;
            }
        }

        public GameStatusDto Evolve(GameStatusDto currentGameStatus)
        {
            uint nextGeneration = currentGameStatus.Generation + 1;
            string[,] nextBoard = (string[,])currentGameStatus.Board.Clone();
            int cols = currentGameStatus.Board.GetLength(0);
            int rows = currentGameStatus.Board.GetLength(1);
            
            for (int i = 0; i < currentGameStatus.Board.GetLength(0); i++)
            {
                for (int j = 0; j < currentGameStatus.Board.GetLength(1); j++)
                {
                    int count = 0;
                    SortedDictionary<string, int> colorCounterDictionary = new SortedDictionary<string, int>();
                    if (i > 0 && !string.IsNullOrEmpty(currentGameStatus.Board[i - 1, j]))
                    {
                        count++;
                        increaseColorCounter(colorCounterDictionary, currentGameStatus.Board[i - 1, j]);
                    }
                    if (i > 0 && j > 0 && !string.IsNullOrEmpty(currentGameStatus.Board[i - 1, j - 1]))
                    {
                        count++;
                        increaseColorCounter(colorCounterDictionary, currentGameStatus.Board[i - 1, j - 1]);
                    }
                    if (i > 0 && j < rows - 1 && !string.IsNullOrEmpty(currentGameStatus.Board[i - 1, j + 1]))
                    {
                        count++;
                        increaseColorCounter(colorCounterDictionary, currentGameStatus.Board[i - 1, j + 1]);
                    }
                    if (j < rows - 1 && !string.IsNullOrEmpty(currentGameStatus.Board[i, j + 1]))
                    {
                        count++;
                        increaseColorCounter(colorCounterDictionary, currentGameStatus.Board[i, j + 1]);
                    }
                    if (j > 0 && !string.IsNullOrEmpty(currentGameStatus.Board[i, j - 1]))
                    {
                        count++;
                        increaseColorCounter(colorCounterDictionary, currentGameStatus.Board[i, j - 1]);
                    }
                    if (i < cols - 1 && !string.IsNullOrEmpty(currentGameStatus.Board[i + 1, j]))
                    {
                        count++;
                        increaseColorCounter(colorCounterDictionary, currentGameStatus.Board[i + 1, j]);
                    }
                    if (i < cols - 1 && j > 0 && !string.IsNullOrEmpty(currentGameStatus.Board[i + 1, j - 1]))
                    {
                        count++;
                        increaseColorCounter(colorCounterDictionary, currentGameStatus.Board[i + 1, j - 1]);
                    }
                    if (i < cols - 1 && j < rows - 1 && !string.IsNullOrEmpty(currentGameStatus.Board[i + 1, j + 1]))
                    {
                        count++;
                        increaseColorCounter(colorCounterDictionary, currentGameStatus.Board[i + 1, j + 1]);
                    }
                    if (!string.IsNullOrEmpty(currentGameStatus.Board[i, j]) && (count < 2 || count > 3))
                    {
                        nextBoard[i, j] = null;
                    }
                    if (string.IsNullOrEmpty(currentGameStatus.Board[i, j]) && count == 3)
                    {
                        //neighbour's most common color
                        nextBoard[i, j] = colorCounterDictionary.Keys.First();
                    }
                }
            }
            GameStatusDto nextGameStatus = new GameStatusDto(currentGameStatus.Columns, currentGameStatus.Rows, nextBoard, nextGeneration);
            return nextGameStatus;
        }

        public async Task<GameStatusDto> ApplyLifeFormAsync(CreateLifeformResourceObject createLifeformResourceObject)
        {
            GameStatusDto currentGameStatus = await _gamestatusService.GetGameStatusAsync();
            GameStatusDto nextGameStatus = new GameStatusDto(currentGameStatus.Columns, currentGameStatus.Rows,
                _lifeformFactory.createLifeform(createLifeformResourceObject.Name.ToUpperInvariant(),
                createLifeformResourceObject.Color,
                createLifeformResourceObject.Col,
                createLifeformResourceObject.Row,
                currentGameStatus.Board)
                , currentGameStatus.Generation);

            await _gamestatusService.SetGameStatusAsync(nextGameStatus);
            return nextGameStatus;
        }

    }
}
