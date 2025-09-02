using Asp.Versioning;
using Basket.Application.Features.ShoppingCart.Commands.CheckoutBasket;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers.V2
{
    [ApiVersion("2")]
    public class BasketController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IMediator mediator, ILogger<BasketController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CheckoutBasket([FromBody] CheckoutBasketCommand checkoutBasketCommand)
        {
            _logger.LogInformation("Basket checkout for user {UserName} will be initialized.", checkoutBasketCommand.UserName);

            await _mediator.Send(checkoutBasketCommand);

            return Accepted();
        }
    }
}
