using AutoMapper;
using Azul.Framework.Data.Configuration;
using Azul.Framework.Data.SqlDapper;
using Dapper;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Queries;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PartNumberRepository : DapperProcedure<PartNumberRepository>, IPartNumberRepository
    {
        private const string _connectionId = nameof(PartNumberRepository);

        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public PartNumberRepository(ILoggerFactory loggerFactory,
                                    IMapper mapper) : base(string.Empty, _connectionId)
        {
            _logger = loggerFactory.CreateLogger<PartNumberRepository>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<PartNumber>> GetAsync()
        {
            IEnumerable<PartNumberEntity> sqlResult;
            try
            {
                sqlResult = await GetAll(null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(PartNumberRepository)} database error | {ex.Message}");
                throw ex;
            }
            _logger.LogInformation($"Obtained data from {nameof(PartNumberRepository)}");

            var result = _mapper.Map<IEnumerable<PartNumber>>(sqlResult);
            return result;
        }

        public async Task<IEnumerable<PartNumber>> GetAsync(DateTime startDate)
        {
            IEnumerable<PartNumberEntity> sqlResult;
            try
            {
                sqlResult = await GetAll(startDate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(PartNumberRepository)} database error | {ex.Message}");
                throw ex;
            }
            _logger.LogInformation($"Obtained data from {nameof(PartNumberRepository)}");

            var result = _mapper.Map<IEnumerable<PartNumber>>(sqlResult);
            return result;
        }

        private async Task<IEnumerable<PartNumberEntity>> GetAll(DateTime? dataStart)
        {
            _logger.LogInformation("Buscando dados no TraxDB...");
            string connectionString = DatabaseConfiguration.Settings.ConnectionSettings.GetConnectionSetting(_connectionId).ConnectionString;
            // Referencia ???
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<PartNumberEntity>(PartNumberQuery.GetAllPartNumber(dataStart));
            }
        }
    }
}
