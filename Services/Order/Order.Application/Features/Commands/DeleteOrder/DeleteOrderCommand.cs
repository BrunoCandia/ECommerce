using MediatR;

namespace Order.Application.Features.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public DeleteOrderCommand(Guid id)
        {
            Id = id;
        }
    }
}
