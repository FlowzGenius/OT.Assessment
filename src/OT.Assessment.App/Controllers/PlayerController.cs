using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Core.Queries.LoadPlayerWagers;
using OT.Assessment.Core.Queries.LoadTopSpenders;
using OT.Assessment.Messaging.Models;
namespace OT.Assessment.App.Controllers
{
  
    [ApiController]
    [Route("/api")]
    public class PlayerController(IPublishEndpoint publishEndpoint, IMediator mediator) : ControllerBase
    {
        //POST api/player/casinowager
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("/api/player/casinowager")]
        public async Task<ActionResult> ProcessCasinoWager([FromBody] CasinoWagerEvent casinoWager, CancellationToken cancellationToken)
        {
            await publishEndpoint.Publish(casinoWager, cancellationToken);

            return Ok();
        }

        //GET api/player/{playerId}/wagers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("/api/player/{playerId}")]
        public async Task<ActionResult> LoadPlayerWagers([FromRoute] Guid playerId, [FromQuery] int page, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            var loadPlayerWagersQuery = new LoadPlayerWagersQuery()
            {
                PlayerId = playerId,
                Page = page,
                PageSize = pageSize
            };

            var response = await mediator.Send(loadPlayerWagersQuery, cancellationToken);

            return Ok(response);
        }

        //GET api/player/topSpenders?count=10
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("/api/player/topSpenders")]
        public async Task<ActionResult> LoadTopSpenders([FromQuery] int count, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new LoadTopSpendersQuery() { Count = count});

            return Ok(response);
        }
    }
}
