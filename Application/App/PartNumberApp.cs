using Application.Commands.CommitDeltaCommand;
using Application.Interfaces;
using Application.Queries.PartNumberQuery;
using AutoMapper;
using CrossCutting.Configurations;
using Infrastructure.Publishers.Interfaces;
using Infrastructure.Publishers.Messages;
using Infrastructure.Services.Contracts;
using Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Events
{
    public class PartNumberApp : IPartNumberApp
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IAzureTablesService  _azureTablesService;
        private readonly IPartNumberPublisher _partNumberPublisher;
        private readonly ILogger _logger;

        public PartNumberApp(IMapper mapper,
                             IMediator mediator,
                             IPartNumberPublisher partNumberPublisher,
                             ILoggerFactory loggerFactory,
                             IAzureTablesService  azureTablesService)
        {                     
            _mapper = mapper;
            _mediator = mediator;
            _logger = loggerFactory.CreateLogger<PartNumberApp>();
            _azureTablesService = azureTablesService;
            _partNumberPublisher = partNumberPublisher;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            DeltaModel deltaRecord = GetDelta();
            // get date reference from azure table
            DateTime dateReference = deltaRecord.GetParsedStartDate();
            // set new date for next run
            deltaRecord.UpdateStartDate(DateTime.UtcNow);

            var partNumbers = await _mediator.Send(new PartNumberQuery() { startDate = dateReference});

            var messages = _mapper.Map<List<PartNumberMessage>>(partNumbers);

            foreach (var message in messages)
                await _partNumberPublisher.PublishMessage(message);

            await _mediator.Send(new CommitDeltaCommand() { DeltaRecord = deltaRecord });
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
            _logger.LogInformation($"{nameof(PartNumberApp)} finished.");
            return Task.CompletedTask;
        }
    }
}
