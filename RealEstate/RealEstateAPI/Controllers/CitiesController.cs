using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RealEstateAPI.Data;
using RealEstateAPI.IRepository;
using RealEstateAPI.ModelsDTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly DatabaseContext _databaseContext;

        public CitiesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CitiesController> logger, DatabaseContext databaseContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _databaseContext = databaseContext;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _unitOfWork.Cities.GetAll(includeProperties: "Country");
            foreach (var city in cities)
            {
                if(city.Photo != null)
                    city.Photo = GetImage(Convert.ToBase64String(city.Photo));
            }
            var results = _mapper.Map<IList<CityDTO>>(cities);
            return Ok(results);
        }
        [HttpGet("{id:int}", Name = "GetCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCity(int id)
        {
            var city = await _databaseContext.Cities.Where(x => x.Id == id).Include(x => x.Country).FirstOrDefaultAsync();
            if (city.Photo != null)
            {
                city.Photo = GetImage(Convert.ToBase64String(city.Photo));
            }
            var result = _mapper.Map<CityDTO>(city);
            return Ok(result);
        }

        /*[HttpPost("save-file")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveFile([FromForm] CreateCityDTO cityDTO)
        {
            if(cityDTO.File == null || cityDTO.File.Length < 1)
            {
                return BadRequest("File not selected");
            }
            City oCity = _mapper.Map<City>(cityDTO);
            using (var ms = new MemoryStream())
            {
                //copy content of file to target stream (memory stream)
                cityDTO.File.CopyTo(ms);
                //writes stream contents into byte array
                var fileBytes = ms.ToArray();
                oCity.Photo = fileBytes;
                //var result = await _databaseContext.Cities.AddAsync(oCity);
                await _unitOfWork.Cities.Insert(oCity);
                await _unitOfWork.Save();
                if (oCity.Id > 0)
                {
                    return Ok();
                }
            }
            return BadRequest("File was found but something brake allong the way");
        }*/

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCity([FromForm] CreateCityDTO cityDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateCity)}");
                return BadRequest();
            }
            if (cityDTO.File == null || cityDTO.File.Length < 1)
            {
                return BadRequest("File not selected");
            }
            City oCity = _mapper.Map<City>(cityDTO);
            using (var ms = new MemoryStream())
            {
                //copy content of file to target stream (memory stream)
                cityDTO.File.CopyTo(ms);
                //writes stream contents into byte array
                var fileBytes = ms.ToArray();
                oCity.Photo = fileBytes;
                //var result = await _databaseContext.Cities.AddAsync(oCity);
                await _unitOfWork.Cities.Insert(oCity);
                await _unitOfWork.Save();
                if (oCity.Id > 0)
                {
                    var createdCity = await _databaseContext.Cities.Where(x => x.Id == oCity.Id).Include(x => x.Country).FirstOrDefaultAsync();
                    if (createdCity.Photo != null)
                    {
                        createdCity.Photo = GetImage(Convert.ToBase64String(createdCity.Photo));
                    }
                    var result = _mapper.Map<CityDTO>(createdCity);
                    return Ok(result);
                }
            }
            return BadRequest("File was found but something brake allong the way");
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCity(int id, [FromForm] UpdateCityDTO cityDTO)
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
            }else if((cityDTO.File == null || cityDTO.File.Length < 1) && cityDTO.Photo == null)
            {
                return BadRequest("Photo is missing");
            }
            _mapper.Map(cityDTO, city);
            //if nothing was changed
            if (cityDTO.Photo != null && cityDTO.File == null)
            { 
                _unitOfWork.Cities.Update(city);
                await _unitOfWork.Save();
            }
            if (cityDTO.File != null)
            {
                using (var ms = new MemoryStream())
                {
                    //copy content of file to target stream (memory stream)
                    cityDTO.File.CopyTo(ms);
                    //writes stream contents into byte array
                    var fileBytes = ms.ToArray();
                    city.Photo = fileBytes;
                    _unitOfWork.Cities.Update(city);
                    await _unitOfWork.Save();
                }
            }
            var updatedCity = await _databaseContext.Cities.Where(x => x.Id == id).Include(x => x.Country).FirstOrDefaultAsync();
            if (updatedCity.Photo != null)
            {
                updatedCity.Photo = GetImage(Convert.ToBase64String(updatedCity.Photo));
            }
            var result = _mapper.Map<CityDTO>(updatedCity);
            return Ok(result);
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

        public static byte[] GetImage(string sBase64String)
        {
            byte[] bytes = null;
            if (!string.IsNullOrEmpty(sBase64String))
            {
                bytes = Convert.FromBase64String(sBase64String);
            }
            return bytes;
        }
    }
}
