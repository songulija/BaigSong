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
using System.Security.Claims;
using System.Text.Json;
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProperties()
        {
            var properties = await _unitOfWork.Properties.GetAll(includeProperties: "PropertyType,RentType,City,User");
            var results = _mapper.Map<IList<PropertyDTO>>(properties);
            return Ok(results);
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropertiesByParams([FromQuery] PaginationParams @params)
        {
            var query = _databaseContext.Properties
                .Include(x => x.PropertyType)
                .Include(x => x.RentType)
                .Include(x => x.City)
                .Include(x => x.User)
                //.Skip((@params.Page - 1) * @params.ItemsPerPage)
                //.Take(@params.ItemsPerPage)
                .AsNoTracking();
            if (@params.PropertyTypeId != 0)
            {
                query = query.Where(x => x.PropertyTypeId == @params.PropertyTypeId);
            }
            if (@params.CityId != 0)
            {
                query = query.Where(x => x.CityId == @params.CityId);
            }
            if (@params.Title != null)
            {
                query = query.Where(x => x.Title.Contains(@params.Title));
            }
            var pagesNumber = await query.CountAsync();
            var properties = await query
                .Skip((@params.Page - 1) * @params.ItemsPerPage)
                .Take(@params.ItemsPerPage)
                .ToListAsync();
            var paginationMetadata = new PaginationMetaData(pagesNumber, @params.Page, @params.ItemsPerPage);
            foreach (var property in properties)
            {
                if (property.Photo != null)
                    property.Photo = GetImage(Convert.ToBase64String(property.Photo));
            }
            var results = _mapper.Map<IList<PropertyDTO>>(properties);
            return Ok(new
            {
                properties = properties,
                pagination = paginationMetadata
            });
        }

        [HttpGet("{id:int}", Name = "GetProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProperty(int id)
        {
            var property = await _databaseContext.Properties.Where(p => p.Id == id)
                .Include(x => x.PropertyType)
                .Include(x => x.FavouriteObjects)
                .Include(x => x.RentType)
                .Include(x => x.City)
                .Include(x => x.User)
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync();
            if (property.Photo != null)
            {
                property.Photo = GetImage(Convert.ToBase64String(property.Photo));
            }
            var result = _mapper.Map<PropertyDTO>(property);
            var claimId = User.Identity.IsAuthenticated;
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString());
                var favouriteProperty = await _unitOfWork.FavouriteProperties.Get(x => x.UserId == userId && x.PropertyId == property.Id);
                result.Liked = favouriteProperty != null ? true : false;
            }
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

        [HttpGet("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropertiesByUserId([FromQuery] PaginationParams @params)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString());
            //to get only selected page items use Skip method. to skip number of rows
            //and use take to get only selected number of rows
            var properties = await _databaseContext.Properties
                .Where(x => x.UserId == userId)
                .Include(x => x.PropertyType)
                .Include(x => x.RentType)
                .Include(x => x.City)
                .Include(x => x.User)
                .Skip((@params.Page - 1) * @params.ItemsPerPage)
                .Take(@params.ItemsPerPage)
                .AsNoTracking()
                .ToListAsync();
            var pagesNumber = await _databaseContext.Properties.Where(x => x.UserId == userId).CountAsync();
            var paginationMetadata = new PaginationMetaData(pagesNumber, @params.Page, @params.ItemsPerPage);
            foreach (var property in properties)
            {
                if (property.Photo != null)
                    property.Photo = GetImage(Convert.ToBase64String(property.Photo));
            }
            //var results = _mapper.Map<IList<PropertyDTO>>(properties);

            //return pagination meta data as response headers
            //Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            return Ok(new {
                properties = properties,
                pagination = paginationMetadata
            });
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
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString());
            propertyDTO.UserId = userId;
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
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString());
            propertyDTO.UserId = userId;
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

        [HttpPut("{id:int}/like")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> LikeProperty(int id, [FromBody] UpdatePropertyDTO propertyDTO)
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
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString());
            propertyDTO.UserId = userId;
            _mapper.Map(propertyDTO, property);
            if (propertyDTO.Like == true)
            {
                propertyDTO.FavouriteObject.UserId = userId;
                var favouriteObject = _mapper.Map<FavouriteProperty>(propertyDTO.FavouriteObject);
                await _unitOfWork.FavouriteProperties.Insert(favouriteObject);
                await _unitOfWork.Save();
                if(favouriteObject.Id < 1)
                {
                    return BadRequest("Submited invalid data. Like not saved.");
                }
            } else if(propertyDTO.Like == false)
            {
                var favouriteProperty = await _unitOfWork.FavouriteProperties.Get(x => x.PropertyId == id && x.UserId == userId);
                if(favouriteProperty == null)
                {
                    return BadRequest("Submited invalid data. Like not deleted");
                }
                await _unitOfWork.FavouriteProperties.Delete(favouriteProperty.Id);
                await _unitOfWork.Save();
            }
            var updatedProperty = await _databaseContext.Properties.Where(x => x.Id == id)
                .Include(x => x.PropertyType)
                .Include(x => x.RentType)
                .Include(x => x.City)
                .Include(x => x.Comments)
                .Include(x => x.User)
                .Include(x => x.FavouriteObjects)
                .FirstOrDefaultAsync();
            var result = _mapper.Map<PropertyDTO>(updatedProperty);
            result.Liked = propertyDTO.Like;
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
            var favouriteProperties = await _unitOfWork.FavouriteProperties.GetAll(x => x.PropertyId == id);
            foreach (var favouriteProperty in favouriteProperties)
            {
                await _unitOfWork.FavouriteProperties.Delete(favouriteProperty.Id);
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
