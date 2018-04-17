using AutoMapper;

namespace QrF.Core.Infrastructure.Cqrs.Queries
{
    public abstract class BaseQueryHandler
    {
        protected readonly IMapper _mapper;

        public BaseQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
