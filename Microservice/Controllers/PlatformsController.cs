using AutoMapper;
using Microservice.Dtos;
using Microservice.Interfaces;
using Microservice.Models;
using Microservice.SyncDataServices.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommadDataClient _commaDataClient;
        public PlatformsController(IPlatformRepo repository, IMapper mapper,ICommadDataClient commanddataclient)
        {
            _repository = repository;
            _mapper = mapper;
            _commaDataClient = commanddataclient;
        }


        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("-->Getting PlatForms");
            var PlatFormItems = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(PlatFormItems));
        }
        [HttpGet("{id}",Name = "GetPlatformById")]


        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformItem = _repository.GetPlatformById(id);
            if (platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }
            return NotFound();

        }
        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformcreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformcreateDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();
            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);


            try
            {
                await _commaDataClient.SendPlatformToCommaand(platformReadDto);

            }
            catch(Exception ex)
            {
                Console.WriteLine($"---> could not send synchronously:{ex.Message}");
            }


            return CreatedAtRoute(nameof(GetPlatformById),new { ID = platformReadDto.id }, platformReadDto);

        }
    }
}
