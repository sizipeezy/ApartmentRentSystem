namespace ApartmentRentSystem.Controllers.Api
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models.Statistics;
    using Microsoft.AspNetCore.Mvc;


    [Route("api/statistics")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService service;

        public StatisticsApiController(IStatisticsService _service)
        {
            service = _service;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetStatistics()
        {
            var model = await service.Total();

            return Ok(model);
        }
    }
}
