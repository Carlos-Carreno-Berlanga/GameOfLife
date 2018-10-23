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
        private readonly IGamestatusService _gamestatusService;
        public GameStatusController(IGamestatusService gamestatusService)
        {
            _gamestatusService = gamestatusService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGameStatusAsync([FromBody]CreateLifeformResourceObject createLifeformResourceObject)
        {
            //_gamestatusService.SetGameStatusAsync(null);
            //ReportDTO report = await _assetManagementPipeMaterialService.UpsertReportAsync(upsertReportResourceObject.ToUpsertReportCommand());
            return Ok();
        }
    }
}
