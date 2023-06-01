using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using PowerPlantApplication.Models;
using PowerPlantApplication.Services;

namespace PowerPlantApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductionPlanController : ControllerBase
    {
        private readonly ICalculatingService _calculatingService;
        private readonly ILogger<ProductionPlanController> _logger;

        public ProductionPlanController(ILogger<ProductionPlanController> logger, ICalculatingService calculatingService)
        {
            _logger = logger;
            _calculatingService = calculatingService;
        }

        /// <summary>
        /// Calculate ProductionPlan.
        /// </summary>
        /// <param name="request">
        /// </param>
        /// <response code="200">Returns the newly calculated ProductionPlan</response>
        /// <response code="400">If the ProductionPlan has any validation errors</response>

        [HttpPost(Name = nameof(ProductionPlanAsync))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IList<PayLoadResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ProductionPlanAsync([FromBody] PayLoadRequest request)
        {
            if (request.Load == 0)
                return BadRequest($"{nameof(request.Load)} cannot be null or empty!");

            var result = _calculatingService.ProductionPlan(request.PowerPlants, request.Fuels, request.Load);

            string json = JsonConvert.SerializeObject(result);

            return StatusCode((int)HttpStatusCode.OK, json);
        }
    }
}