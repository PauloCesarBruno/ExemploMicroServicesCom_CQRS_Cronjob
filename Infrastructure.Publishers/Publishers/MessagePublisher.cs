using Azul.Framework.Events;
using Azul.Framework.Events.Configuration;
using Infrastructure.Publishers.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Publishers.Publishers 
{
    public class MessagePublisher<T> : EventPublisher<T>, IMessagePublisher<T> where T : class
    {
        protected readonly ILogger<T> _logger;
        public virtual EventSetting Settings => EventConfiguration.Settings.EventSetting.FirstOrDefault(cfg => cfg.Id == GetType().Name);
        public override string TopicName => Settings is null ? string.Empty : Settings.Parameters.FirstOrDefault(cfg => cfg.Key == "Topic").Value.ToString();
        public override string ConnectionId => GetType().Name;

        public MessagePublisher(ILogger<T> logger)
        {
            _logger = logger;
        }


        public async Task PublishMessage(T message)
        {
            try
            {
                await PublishAsync(message);
                _logger.LogInformation($"Published: {JsonConvert.SerializeObject(message)}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while posting message: {JsonConvert.SerializeObject(message)}.\n\rError message details: '{ex.Message}'");
            }
        }
    }
}
