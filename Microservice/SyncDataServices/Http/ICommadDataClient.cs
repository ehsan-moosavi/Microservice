using Microservice.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.SyncDataServices.Http
{
    public interface ICommadDataClient
    {
        Task SendPlatformToCommaand(PlatformReadDto plat);
      
    }
}
