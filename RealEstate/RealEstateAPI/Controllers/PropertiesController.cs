using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class PropertiesController : ControllerBase
    {
        // IUnitOfWork registers for every variation of GenericRepository
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PropertiesController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly DatabaseContext _databaseContext;

        public PropertiesController(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<PropertiesController> logger, IWebHostEnvironment environment, DatabaseContext databaseContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _environment = environment;
            _databaseContext = databaseContext;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProperties()
        {

            var properties = await _unitOfWork.Properties.GetAll(includeProperties: "PropertyType,RentType,City,User");
            foreach (var property in properties)
            {
                if (property.Photo != null)
                    property.Photo = GetImage(Convert.ToBase64String(property.Photo));
            }
            var results = _mapper.Map<IList<PropertyDTO>>(properties);
            return Ok(results);
        }
        [HttpGet("{id:int}", Name = "GetProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProperty(int id)
        {
            var property = await _databaseContext.Properties.Where(p => p.Id == id)
                .Include(x => x.PropertyType)
                .Include(x => x.RentType)
                .Include(x => x.City)
                .Include(x => x.Comments)
                .Include(x => x.User)
                .FirstOrDefaultAsync();
            if (property.Photo != null)
            {
                property.Photo = GetImage(Convert.ToBase64String(property.Photo));
            }
            var result = _mapper.Map<PropertyDTO>(property);
            return Ok(result);
        }
        [HttpGet("propertyType/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropertiesByPropertyTypeId(int id)
        {
            var properties = await _unitOfWork.Properties.GetAll(f => f.PropertyTypeId == id,
                includeProperties: "PropertyType,RentType,City");
            foreach (var property in properties)
            {
                if (property.Photo != null)
                    property.Photo = GetImage(Convert.ToBase64String(property.Photo));
            }
            var results = _mapper.Map<IList<PropertyDTO>>(properties);
            return Ok(results);
        }

        [HttpGet("rentType/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropertiesByRentTypeId(int id)
        {
            var properties = await _unitOfWork.Properties.GetAll(f => f.RentTypeId == id,
                includeProperties: "PropertyType,RentType,City");
            foreach (var property in properties)
            {
                if (property.Photo != null)
                    property.Photo = GetImage(Convert.ToBase64String(property.Photo));
            }
            var results = _mapper.Map<IList<PropertyDTO>>(properties);
            return Ok(results);
        }

        [HttpGet("user/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropertiesByUserId(int id)
        {
            var properties = await _unitOfWork.Properties.GetAll(f => f.UserId == id,
                includeProperties: "PropertyType,RentType,City");
            foreach (var property in properties)
            {
                if (property.Photo != null)
                    property.Photo = GetImage(Convert.ToBase64String(property.Photo));
            }
            var results = _mapper.Map<IList<PropertyDTO>>(properties);
            return Ok(results);
        }

        [HttpGet("city/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropertiesByCityId(int id)
        {
            var properties = await _unitOfWork.Properties.GetAll(f => f.CityId == id,
                includeProperties: "PropertyType,RentType,City");
            foreach (var property in properties)
            {
                if (property.Photo != null)
                    property.Photo = GetImage(Convert.ToBase64String(property.Photo));
            }
            var results = _mapper.Map<IList<PropertyDTO>>(properties);
            return Ok(results);
        }

        [HttpGet("country/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropertiesByCountryId(int id)
        {
            var properties = await _unitOfWork.Properties.GetAll(f => f.City.CountryId== id,
                includeProperties: "PropertyType,RentType,City");
            foreach (var property in properties)
            {
                if (property.Photo != null)
                    property.Photo = GetImage(Convert.ToBase64String(property.Photo));
            }
            var results = _mapper.Map<IList<PropertyDTO>>(properties);
            return Ok(results);
        }

        [HttpGet("most-liked-properties")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMostLikedProperties()
        {
            /* var favouriteProperties = await _databaseContext.FavouriteProperties.GroupBy();*/
            var favouriteProperties = await _databaseContext.FavouriteProperties.GroupBy(d => d.PropertyId)
                .Select(
                    g => new
                    {
                        Key = g.Key,
                        Count = g.Count()
                    }).OrderByDescending(x => x.Count).Take(5).ToListAsync();
            var properties = new List<PropertyDTO>();
            foreach (var favouriteProperty in favouriteProperties)
            {
                var property = await _unitOfWork.Properties.Get(
                    x => x.Id == favouriteProperty.Key,
                    includeProperties: "PropertyType,RentType,City");
                if (property.Photo != null)
                {
                    property.Photo = GetImage(Convert.ToBase64String(property.Photo));
                }
                var propertyDTO = _mapper.Map<PropertyDTO>(property);
                propertyDTO.NumberOfLikes = favouriteProperty.Count;
                properties.Add(propertyDTO);
            }
            return Ok(properties);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProperty([FromForm] CreatePropertyDTO propertyDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateProperty)}");
                return BadRequest();
            }
            if (propertyDTO.File == null || propertyDTO.File.Length < 1)
            {
                return BadRequest("File not selected");
            }
            Property oProperty = _mapper.Map<Property>(propertyDTO);
            using (var ms = new MemoryStream())
            {
                //copy content of file to target stream (memory stream)
                propertyDTO.File.CopyTo(ms);
                //writes stream contents into byte array
                var fileBytes = ms.ToArray();
                oProperty.Photo = fileBytes;
                await _unitOfWork.Properties.Insert(oProperty);
                await _unitOfWork.Save();
                if (oProperty.Id > 0)
                {
                    var createdProperty = await _databaseContext.Properties.Where(x => x.Id == oProperty.Id).Include(x => x.PropertyType).Include(x => x.RentType).Include(x => x.City).Include(x => x.Comments).Include(x => x.User).FirstOrDefaultAsync();
                    if (createdProperty.Photo != null)
                    {
                        createdProperty.Photo = GetImage(Convert.ToBase64String(createdProperty.Photo));
                    }
                    var result = _mapper.Map<PropertyDTO>(createdProperty);
                    return Ok(result);
                }
            }
            return BadRequest("File was found but something brake allong the way");
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProperty(int id, [FromForm] UpdatePropertyDTO propertyDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE ettempt in {nameof(UpdateProperty)}");
                return BadRequest();
            }
            var property = await _unitOfWork.Properties.Get(f => f.Id == id);
            if (property == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProperty)}");
                return BadRequest("Submited invalid data");
            }
            else if ((propertyDTO.File == null || propertyDTO.File.Length < 1) && propertyDTO.Photo == null)
            {
                return BadRequest("Photo is missing");
            }
            _mapper.Map(propertyDTO, property);
            //if nothing was changed
            if (propertyDTO.Photo != null && propertyDTO.File == null)
            {
                _unitOfWork.Properties.Update(property);
                await _unitOfWork.Save();
            }
            if (propertyDTO.File != null)
            {
                using (var ms = new MemoryStream())
                {
                    //copy content of file to target stream (memory stream)
                    propertyDTO.File.CopyTo(ms);
                    //writes stream contents into byte array
                    var fileBytes = ms.ToArray();
                    property.Photo = fileBytes;
                    _unitOfWork.Properties.Update(property);
                    await _unitOfWork.Save();
                }
            }
            var updatedProperty = await _databaseContext.Properties.Where(x => x.Id == id).Include(x => x.PropertyType).Include(x => x.RentType).Include(x => x.City).Include(x => x.Comments).Include(x => x.User).FirstOrDefaultAsync();
            if (updatedProperty.Photo != null)
            {
                updatedProperty.Photo = GetImage(Convert.ToBase64String(updatedProperty.Photo));
            }
            var result = _mapper.Map<PropertyDTO>(updatedProperty);
            return Ok(result);
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var property = await _unitOfWork.Properties.Get(f => f.Id == id);
            if (property == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteProperty)}");
                return BadRequest();
            }
            await _unitOfWork.Properties.Delete(id);
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
