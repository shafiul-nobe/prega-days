using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PregaDays.Mapper;
using PregaDays.Models.Domain;
using PregaDays.Models.DTO;
using PregaDays.Repositories;

namespace PregaDays.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DescriptionController : ControllerBase
    {
        private readonly IDescriptionRepository repository;
        private readonly IMapper mapper;
        public DescriptionController(IDescriptionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            var days = await repository.GetAllAsync();
            return Ok(mapper.Map<List<DayDto>>(days));
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] AddDayRequestDto addDayRequestDto)
        {
            var day = mapper.Map<Day>(addDayRequestDto);
            await repository.CreateAsync(day);
            var dayDto = mapper.Map<DayDto>(day);
            return Ok(dayDto);
        }

    }
}
