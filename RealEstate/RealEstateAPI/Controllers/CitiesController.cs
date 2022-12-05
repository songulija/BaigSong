using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealEstateAPI.Data;
using RealEstateAPI.IRepository;
using RealEstateAPI.ModelsDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        // IUnitOfWork registers for every variation of GenericRepository
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CitiesController> _logger;

        public CitiesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CitiesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _unitOfWork.Cities.GetAll();
            var results = _mapper.Map<IList<CityDTO>>(cities);
            return Ok(results);
        }
        [HttpGet("{id:int}", Name = "GetCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCity(int id)
        {
            var city = await _unitOfWork.Cities.Get(p => p.Id == id);
            var result = _mapper.Map<CityDTO>(city);
            return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCity([FromBody] CreateCityDTO cityDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateCity)}");
                return BadRequest();
            }
            var city = _mapper.Map<City>(cityDTO);
            await _unitOfWork.Cities.Insert(city);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetCity", new { id = city.Id }, city);
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] UpdateCityDTO cityDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE ettempt in {nameof(UpdateCity)}");
                return BadRequest();
            }
            var city = await _unitOfWork.Cities.Get(f => f.Id == id);
            if (city == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCity)}");
                return BadRequest("Submited invalid data");
            }
            //map favouritePropertyDTO to favouriteProperty domain object. puts all fields values from dto to favouriteProperty object
            _mapper.Map(cityDTO, city);
            _unitOfWork.Cities.Update(city);
            await _unitOfWork.Save();

            return NoContent();
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _unitOfWork.Cities.Get(f => f.Id == id);
            if (city == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCity)}");
                return BadRequest();
            }
            await _unitOfWork.Cities.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
