using Basket.Application.Features.ShoppingCart.Commands.CreateShoppingCart;
using Basket.Application.Features.ShoppingCart.Commands.DeleteBasketByUserName;
using Basket.Application.Features.ShoppingCart.Queries.GetBasketByUserName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("[action]/{userName}", Name = "GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasketByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return BadRequest("Username cannot be null or empty.");
            }

            var query = new GetBasketByUserNameQuery(userName)
            {
                UserName = userName // Set required member explicitly
            };

            var shoppingCart = await _mediator.Send(query);

            if (shoppingCart == null)
            {
                return NotFound($"Basket for user '{userName}' not found.");
            }

            return Ok(shoppingCart);
        }

        [HttpPost("CreateBasket")]
        [ProducesResponseType(typeof(ShoppingCartResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ShoppingCartResponse>> CreateBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
        {
            if (createShoppingCartCommand is null || string.IsNullOrWhiteSpace(createShoppingCartCommand.UserName))
            {
                return BadRequest("Invalid shopping cart data.");
            }

            var basket = await _mediator.Send(createShoppingCartCommand);

            return Ok(basket);
        }

        [HttpDelete("DeleteBasketByUserName/{userName}")]
        //[Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return BadRequest("Username cannot be null or empty.");
            }

            var command = new DeleteBasketByUserNameCommand(userName)
            {
                UserName = userName
            };

            await _mediator.Send(command);

            return Ok();
        }
    }
}
