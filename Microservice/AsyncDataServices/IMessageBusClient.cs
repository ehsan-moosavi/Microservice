using Microservice.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void publishnewPlatform(PlatformPublishedDto platforPublishedDto);
    }
}
