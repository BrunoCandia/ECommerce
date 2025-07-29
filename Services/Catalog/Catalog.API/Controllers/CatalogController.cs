using Catalog.Application.Features.Brands.Queries.GetBrands;
using Catalog.Application.Features.Products.Commands.CreateProduct;
using Catalog.Application.Features.Products.Commands.DeleteProduct;
using Catalog.Application.Features.Products.Commands.UpdateProduct;
using Catalog.Application.Features.Products.Queries.GetProductById;
using Catalog.Application.Features.Products.Queries.GetProductByName;
using Catalog.Application.Features.Products.Queries.GetProducts;
using Catalog.Application.Features.Products.Queries.GetProductsByBrand;
using Catalog.Application.Features.Products.Queries.GetProductsPaginated;
using Catalog.Application.Features.Types.Queries.GetTypes;
using Catalog.Core.Helper;
using Catalog.Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    public class CatalogController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponse>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id)
            {
                Id = id // Set required member explictly
            };

            var product = await _mediator.Send(query);

            return Ok(product);
        }

        [HttpGet]
        [Route("[action]/{name}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductByName(string name)
        {
            var query = new GetProductByNameQuery(name)
            {
                Name = name // Set required member explictly
            };

            var products = await _mediator.Send(query);

            return Ok(products);
        }

        [HttpGet]
        [Route("GetProductsPaginated")]
        [ProducesResponseType(typeof(Pagination<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<ProductResponse>>> GetProductsPaginated([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query = new GetProductsPaginatedQuery(catalogSpecParams)
            {
                CatalogSpecParams = catalogSpecParams // Set required member explictly
            };

            var products = await _mediator.Send(query);

            return Ok(products);
        }

        [HttpGet]
        [Route("GetProducts")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts()
        {
            var query = new GetProductsQuery();

            var products = await _mediator.Send(query);

            return Ok(products);
        }

        [HttpGet]
        [Route("GetBrands")]
        [ProducesResponseType(typeof(IEnumerable<BrandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<BrandResponse>>> GetBrands()
        {
            var query = new GetBrandsQuery();

            var brands = await _mediator.Send(query);

            return Ok(brands);
        }

        [HttpGet]
        [Route("GetTypes")]
        [ProducesResponseType(typeof(IEnumerable<TypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<BrandResponse>>> GetTypes()
        {
            var query = new GetTypesQuery();

            var types = await _mediator.Send(query);

            return Ok(types);
        }

        [HttpGet]
        [Route("[action]/{brand}", Name = "GetProductsByBrand")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByBrand(string brand)
        {
            var query = new GetProductsByBrandQuery(brand)
            {
                Brand = brand // Set required member explictly
            };

            var products = await _mediator.Send(query);

            return Ok(products);
        }

        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand createProductCommand)
        {
            if (createProductCommand is null)
            {
                return BadRequest("Product cannot be null.");
            }

            var product = await _mediator.Send(createProductCommand);

            return Ok(product);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand updateProductCommand)
        {
            if (updateProductCommand is null)
            {
                return BadRequest("Product cannot be null.");
            }

            var isUpdated = await _mediator.Send(updateProductCommand);

            return Ok(isUpdated);
        }

        [HttpDelete]
        [Route("[action]/{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteProduct(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Product ID cannot be null or empty.");
            }

            var command = new DeleteProductCommand(id)
            {
                Id = id // Set required member explictly
            };

            var isDeleted = await _mediator.Send(command);

            return Ok(isDeleted);
        }
    }
}
