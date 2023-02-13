using Infrastructure.Publishers.Interfaces;
using Infrastructure.Publishers.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Publishers.Publishers
{
    public class PartNumberQuantityPublisher : MessagePublisher<PartNumberQuantityMessages>, IPartNumberQuantityPublisher
    {
        public PartNumberQuantityPublisher(ILogger<PartNumberQuantityMessages> logger) : base(logger)
        {

        }
    }
}
