using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TechParts.API.Data;
using TechParts.API.Models;
using TechParts.API.Dtos;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using TechParts.API.Helpers;
using TechParts.API.ActionFilters;

namespace TechParts.API.Controllers
{
    [ServiceFilter(typeof(LastActiveUpdater))]
    [ApiController]
    [Route("[controller]")]
    public class PartController : ControllerBase
    {
        private IPartRepository _partRepo { get; }
        
        private IMapper _mapper { get; }

        public PartController(IPartRepository partRepo, IMapper mapper)
        {
            this._partRepo = partRepo;
            this._mapper = mapper;
        }
        
        [HttpGet("{id}", Name = "GetPart")]
        public async Task<IActionResult> GetPart(int id)
        {
            System.Console.WriteLine("Getting part " + id);
            
            var partFromRepo = await _partRepo.GetPart(id);
            
            if(partFromRepo == null)
            {
                return NotFound();
            }

            return Ok(partFromRepo);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParts()
        {
            var parts = await _partRepo.GetAllParts();

            return Ok(parts);
        }

        [HttpGet("Simple")]
        public async Task<IActionResult> GetAllPartsSimple()
        {
            var parts = await _partRepo.GetAllParts();

            return Ok(_mapper.Map<IEnumerable<PartSimpleDto>>(parts));
        }

        [HttpGet("Paged")]
        public async Task<IActionResult> GetAllPartsPaged([FromQuery]PagedParams pagedParams)
        {

            System.Console.WriteLine("Items per page: " + pagedParams.ItemPerPage);
            System.Console.WriteLine("Current page: " + pagedParams.CurrentPage);
            
            var nbOfParts = await _partRepo.GetNumberOfParts();

            pagedParams.Calculate(nbOfParts);

            var pagedParts = _partRepo.GetAllPartsPaged(pagedParams);
            
            Response.AddPagination(pagedParams);

            return Ok(pagedParts);
        }
    }
}