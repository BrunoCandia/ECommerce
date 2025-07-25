using AutoMapper;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Features.Types.Queries.GetTypes
{
    public class GetTypesQueryHandler : IRequestHandler<GetTypesQuery, IEnumerable<TypeResponse>>
    {
        private readonly ITypeRepository _typeRepository;
        private readonly IMapper _mapper;

        public GetTypesQueryHandler(ITypeRepository typeRepository, IMapper mapper)
        {
            _typeRepository = typeRepository ?? throw new ArgumentNullException(nameof(typeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<TypeResponse>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _typeRepository.GetTypesAsync();
            var typeResponses = _mapper.Map<IEnumerable<TypeResponse>>(types);

            return typeResponses;
        }
    }
}
