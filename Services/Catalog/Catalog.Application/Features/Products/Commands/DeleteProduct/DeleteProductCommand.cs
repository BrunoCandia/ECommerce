using MediatR;

namespace Catalog.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public required string Id { get; set; }

        public DeleteProductCommand(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id), "Product ID cannot be null");
        }
    }
}
