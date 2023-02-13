using Infrastructure.Services.AzureTables.Interfaces;
using System;

namespace Infrastructure.Services.AzureTables.Models
{
    public abstract class BaseAzureTablesModel : IBaseAzureTablesModel
    {
        /// <summary>
        /// Primeiro campo da chave composta da tabela do Azure Tables.
        /// Valor atribuido na propriedade '_partition' do seu service.
        /// </summary>
        public string PartitionKey { get; set; }
        /// <summary>
        /// Segundo valor da chave composta da tabela do Azure Tables.
        /// Será gerado um Guid automático.
        /// </summary>
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Data de registro.
        /// Se nulo durante o insert o Azure Tables irá gerar o valor automaticamente.
        /// </summary>
        public string Timestamp { get; set; }

        public bool IsValid() => string.IsNullOrEmpty(Timestamp) ? false : true;
    }
}