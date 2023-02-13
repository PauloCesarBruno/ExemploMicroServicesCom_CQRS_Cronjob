using Infrastructure.Services.Contracts;
using Infrastructure.Services.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.CommitDeltaCommand
{
    public class CommitDeltaCommand : IRequest
    {
        public DeltaModel DeltaRecord { get; set; }
    }

    public class CommitDeltaCommandHandler : IRequestHandler<CommitDeltaCommand, Unit>
    {

        private readonly IAzureTablesService _azureTablesService;

        public CommitDeltaCommandHandler(IAzureTablesService azureTablesService)
        {
            _azureTablesService = azureTablesService;
        }

        public async Task<Unit> Handle(CommitDeltaCommand request, CancellationToken cancellationToken)
        {
            if (request.DeltaRecord is null)
                await _azureTablesService.Commit();
            else
                await _azureTablesService.UpsertEntity(request.DeltaRecord);
            return Unit.Value;
        }
    }
}
