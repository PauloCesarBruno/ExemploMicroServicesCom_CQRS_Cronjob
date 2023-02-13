using Domain.Models;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.InventoryQuery
{
    public class PartNumberQuantity : IRequest<IEnumerable<Domain.Models.PartNumberQuantity>>
    {
        public DateTime startDate { get; set; }
    }

    public class InventoryQueryHandler : IRequestHandler<PartNumberQuantity, IEnumerable<Domain.Models.PartNumberQuantity>>
    {
        private readonly IPartNumberQuantityRepository _inventoryRepository;

        public InventoryQueryHandler(IPartNumberQuantityRepository inventoriRepository)
        {
            _inventoryRepository = inventoriRepository;
        }               

        public async Task<IEnumerable<Domain.Models.PartNumberQuantity>> Handle(PartNumberQuantity request, CancellationToken cancellationToken)
        {
            return await _inventoryRepository.GetAsync(request.startDate);
        }
    }
}
