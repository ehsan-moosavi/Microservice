using Microservice.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservice.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommadDataClient

    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpclient,IConfiguration configuration)
        {
            _httpClient = httpclient;
            _configuration = configuration;
        }


        public async Task SendPlatformToCommaand(PlatformReadDto plat)
        {
            var HttpContent = new StringContent(
                JsonSerializer.Serialize(plat), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}/api/c/platforms",HttpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--->sync POST to CommandService was ok!");
            }
            else
            {
                Console.Write("--->sync POST to CommandService was Not ok!");
            }
        }
    }
}
