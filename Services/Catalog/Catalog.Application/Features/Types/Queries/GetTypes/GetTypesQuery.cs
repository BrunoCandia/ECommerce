using MediatR;

namespace Catalog.Application.Features.Types.Queries.GetTypes
{
    public class GetTypesQuery : IRequest<IEnumerable<TypeResponse>>
    {
    }
}
