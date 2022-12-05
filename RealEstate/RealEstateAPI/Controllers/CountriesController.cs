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
    public class CountriesController : ControllerBase
    {
        // IUnitOfWork registers for every variation of GenericRepository
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CountriesController> _logger;

        public CountriesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CountriesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _unitOfWork.Countries.GetAll();
            var results = _mapper.Map<IList<CountryDTO>>(countries);
            return Ok(results);
        }
        [HttpGet("{id:int}", Name = "GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountry(int id)
        {
            var country = await _unitOfWork.Countries.Get(p => p.Id == id);
            var result = _mapper.Map<CountryDTO>(country);
            return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateCountry)}");
                return BadRequest();
            }
            var country = _mapper.Map<Country>(countryDTO);
            await _unitOfWork.Countries.Insert(country);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetPropertyType", new { id = country.Id }, country);
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE ettempt in {nameof(UpdateCountry)}");
                return BadRequest();
            }
            var country = await _unitOfWork.Countries.Get(f => f.Id == id);
            if (country == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCountry)}");
                return BadRequest("Submited invalid data");
            }
            //map favouritePropertyDTO to favouriteProperty domain object. puts all fields values from dto to favouriteProperty object
            _mapper.Map(countryDTO, country);
            _unitOfWork.Countries.Update(country);
            await _unitOfWork.Save();

            return NoContent();
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _unitOfWork.Countries.Get(f => f.Id == id);
            if (country == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCountry)}");
                return BadRequest();
            }
            await _unitOfWork.Countries.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
