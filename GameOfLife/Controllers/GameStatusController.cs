using GameOfLife.ResourceObjects;
using GameOfLife.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife.Controllers
{
    [Route("api/[controller]")]
    public class GameStatusController : Controller
    {
        private readonly IGameEvolutionService _gameEvolutionService;
        public GameStatusController(IGameEvolutionService gameEvolutionService)
        {
            _gameEvolutionService = gameEvolutionService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGameStatusAsync([FromBody]CreateLifeformResourceObject createLifeformResourceObject)
        {
            var result = await _gameEvolutionService.ApplyLifeFormAsync(createLifeformResourceObject);
            return Ok(result);
        }
    }
}
