using Infrastructure.Publishers.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Publishers.Interfaces
{
    public interface IPartNumberQuantityPublisher : IMessagePublisher<PartNumberQuantityMessages>
    {
    }
}
