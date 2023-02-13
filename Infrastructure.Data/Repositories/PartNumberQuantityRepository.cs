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
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PartNumberQuantityRepository : DapperProcedure<PartNumberQuantityRepository>, IPartNumberQuantityRepository
    {
        private const string _connectionId = nameof(PartNumberRepository);

        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public PartNumberQuantityRepository(ILoggerFactory loggerFactory,
                                    IMapper mapper) : base(string.Empty, _connectionId)
        {
            _logger = loggerFactory.CreateLogger<PartNumberQuantityRepository>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<PartNumberQuantity>> GetAsync()
        {
            IEnumerable<PartNumberQuantityEntity> sqlResult;
            try
            {
                sqlResult = await GetAll(null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(PartNumberQuantityRepository)} database error | {ex.Message}");
                throw ex;
            }
            _logger.LogInformation($"Obtained data from {nameof(PartNumberQuantityRepository)}");

            var result = _mapper.Map<IEnumerable<PartNumberQuantity>>(sqlResult);
            return result;
        }

        public async Task<IEnumerable<PartNumberQuantity>> GetAsync(DateTime startDate)
        {
            IEnumerable<PartNumberQuantityEntity> sqlResult;
            try
            {
                sqlResult = await GetAll(startDate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(PartNumberQuantityRepository)} database error | {ex.Message}");
                throw ex;
            }
            _logger.LogInformation($"Obtained data from {nameof(PartNumberQuantityRepository)}");

            var result = _mapper.Map<IEnumerable<PartNumberQuantity>>(sqlResult);
            return result;
        }

        private async Task<IEnumerable<PartNumberQuantityEntity>> GetAll(DateTime? dataStart)
        {
            _logger.LogInformation("Buscando dados no TraxDB...");
            string connectionString = DatabaseConfiguration.Settings.ConnectionSettings.GetConnectionSetting(_connectionId).ConnectionString;
            // Referencia ???
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                string sql = PartNumberQuery.GetAllPartNumberQuantity(dataStart);
                return await connection.QueryAsync<PartNumberQuantityEntity>(sql);
            }
        }
    }
}
