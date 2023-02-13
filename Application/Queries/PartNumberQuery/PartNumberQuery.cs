using Domain.Models;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.PartNumberQuery
{
    public class PartNumberQuery : IRequest<IEnumerable<PartNumber>>
    {
        public DateTime startDate { get; set; }
    }

    public class PartNumberQueryHandler : IRequestHandler<PartNumberQuery, IEnumerable<PartNumber>>
    {
        private readonly IPartNumberRepository _partNumberRepository;

        public PartNumberQueryHandler(IPartNumberRepository partNumberRepository)
        {
            _partNumberRepository = partNumberRepository;
        }

        public async Task<IEnumerable<PartNumber>> Handle(PartNumberQuery request, CancellationToken cancellationToken)
        {
            return await _partNumberRepository.GetAsync(request.startDate);
        }
    }
}
