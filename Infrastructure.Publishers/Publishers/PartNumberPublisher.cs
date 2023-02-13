using Infrastructure.Publishers.Interfaces;
using Infrastructure.Publishers.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Publishers.Publishers
{
    public class PartNumberPublisher : MessagePublisher<PartNumberMessage>, IPartNumberPublisher
    {
        public PartNumberPublisher(ILogger<PartNumberMessage> logger) : base(logger)
        {

        }
    }
}
