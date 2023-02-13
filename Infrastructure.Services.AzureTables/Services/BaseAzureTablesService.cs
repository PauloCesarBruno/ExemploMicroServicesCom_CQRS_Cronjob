using Azure;
using Azure.Data.Tables;
using Infrastructure.Services.AzureTables.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services.AzureTables.Services
{
    public abstract class BaseAzureTablesService<T> where T : IBaseAzureTablesModel, new()
    {
        private TableClient _tableClient;
        protected abstract string _partition { get; }
        private readonly ILogger _logger;

        public BaseAzureTablesService(TableClient tableClient, ILoggerFactory loggerFactory)
        {
            _tableClient = tableClient;
            _logger = loggerFactory.CreateLogger<T>();
        }

        public IEnumerable<T> GetRowsFromPartitionKey()
        {
            Pageable<TableEntity> entities = _tableClient.Query<TableEntity>();
            return entities.Select(e => MapTableEntityToDataModel(e)).OfType<T>();
        }

        protected T MapTableEntityToDataModel(TableEntity tableEntity)
        {
            if (tableEntity.PartitionKey.Equals(_partition))
            {
                var dictionaryProp = new Dictionary<string, object>();

                foreach (var key in tableEntity.Keys)
                {
                    if (IsETagKey(key))
                        continue;
                    dictionaryProp.Add(key, tableEntity[key].ToString());
                }
                
                return MapFromDictionary(dictionaryProp);
            }

            return default(T);
        }

        public async Task<bool> UpsertEntity(T model)
        {
            TableEntity entity = new TableEntity();
            entity.PartitionKey = _partition;
            entity.RowKey = model is null ? Guid.NewGuid().ToString() : model.RowKey;
            foreach (var property in model.GetType().GetProperties().ToList())
            {
                if (property.GetValue(model) == null)
                    continue;
                entity[property.Name] = property.GetValue(model).ToString();
            }
            await _tableClient.UpsertEntityAsync(entity);
            _logger.LogInformation($"Entity Upserted in AzureTable: {JsonConvert.SerializeObject(entity)}");
            return true;
        }

        public async Task<bool> UpsertEntity(string rowKey)
        {   
            TableEntity entity = new TableEntity();
            entity.PartitionKey = _partition;
            entity.RowKey = rowKey ?? Guid.NewGuid().ToString();

            await _tableClient.UpsertEntityAsync(entity);
            _logger.LogInformation($"Entity Upserted in AzureTable: {JsonConvert.SerializeObject(entity)}");
            return true;
        }

        private T MapFromDictionary(IDictionary<string, object> dictionary)
        {
            var entity = new T();

            if (dictionary == null)
                return entity;

            foreach (var entry in dictionary)
            {
                var propertyInfo = entity.GetType().GetProperty(entry.Key);
                if (propertyInfo != null)
                    propertyInfo.SetValue(entity, entry.Value, null);
            }
            return entity;
        }

        private bool IsETagKey(string key) => key == "odata.etag";
    }
}