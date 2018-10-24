using GameOfLife.Dtos;
using GameOfLife.ResourceObjects;
using System.Threading.Tasks;

namespace GameOfLife.Services.Interfaces
{
    public interface IGameEvolutionService
    {
        GameStatusDto Evolve(GameStatusDto currentGameStatus);

        Task<GameStatusDto> ApplyLifeFormAsync(CreateLifeformResourceObject createLifeformResourceObject);
    }
}
