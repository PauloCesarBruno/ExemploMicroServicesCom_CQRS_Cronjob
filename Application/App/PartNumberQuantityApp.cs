using Application.Commands.CommitDeltaCommand;
using Application.Interfaces;
using Application.Queries.InventoryQuery;
using AutoMapper;
using CrossCutting.Configurations;
using Infrastructure.Publishers.Interfaces;
using Infrastructure.Publishers.Messages;
using Infrastructure.Services.Contracts;
using Infrastructure.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.App
{
    public class PartNumberQuantityApp : IPartNumberQuantityApp
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IAzureTablesService _azureTablesService;
        private readonly IPartNumberQuantityPublisher _inventoryPublisher;

        public PartNumberQuantityApp(IMapper mapper,
                             IMediator mediator,
                             IPartNumberQuantityPublisher inventoryPublisher,
                             IAzureTablesService azureTablesService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _inventoryPublisher = inventoryPublisher;
            _azureTablesService = azureTablesService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            DeltaModel deltaRecord = GetDelta();
            // get date reference from azure table
            DateTime dateReference = deltaRecord.GetParsedStartDate();
            // set new date for next run
            deltaRecord.UpdateStartDate(DateTime.UtcNow);

            var inventory = await _mediator.Send(new PartNumberQuantity() { startDate = dateReference });
           var messages = _mapper.Map<List<PartNumberQuantityMessages>>(inventory);

            foreach (var message in messages)
                await _inventoryPublisher.PublishMessage(message);

            await _mediator.Send(new CommitDeltaCommand());
        }

        private DeltaModel GetDelta()
        {
            var result = _azureTablesService.GetRowsFromPartitionKey().FirstOrDefault();

            if (result != null)
                return result;
            DeltaModel startDelta = new DeltaModel();
            SetDefaultStartDate(startDelta);
            return startDelta;
        }

        private void SetDefaultStartDate(DeltaModel delta)
        {
            DateTime defaultDateUtc = Convert.ToDateTime(AzureTableInitialSettings.Settings.Parameters.GetStartDateOnUTC());
            delta.UpdateStartDate(defaultDateUtc);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
