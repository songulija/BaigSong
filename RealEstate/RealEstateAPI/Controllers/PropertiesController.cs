using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
    public class PropertiesController : ControllerBase
    {
        // IUnitOfWork registers for every variation of GenericRepository
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PropertiesController> _logger;
        private readonly IWebHostEnvironment _environment;
        //private readonly DatabaseContext _databaseContext;

        public PropertiesController(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<PropertiesController> logger, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _environment = environment;
            //_databaseContext = databaseContext;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProperties()
        {

            var properties = await _unitOfWork.Properties.GetAll(includeProperties: "PropertyType,RentType");
            var results = _mapper.Map<IList<PropertyDTO>>(properties);
            return Ok(results);
        }
        [HttpGet("{id:int}", Name = "GetProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProperty(int id)
        {
            var property = await _unitOfWork.Properties.Get(p => p.Id == id,
                includeProperties: "PropertyType,RentType,Comments,Journals,FavouriteObjects");
            //var property = await _databaseContext.Properties.Where(x => x.Id == id).Include(x => x.PropertyType).SingleOrDefaultAsync();
            var result = _mapper.Map<PropertyDTO>(property);
            return Ok(result);
        }
        [HttpGet("propertyType/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropertiesByPropertyTypeId(int id)
        {
            var properties = await _unitOfWork.Properties.GetAll(f => f.PropertyTypeId == id,
                includeProperties: "PropertyType,RentType");
            var results = _mapper.Map<IList<PropertyDTO>>(properties);
            return Ok(results.Count);
        }

        [HttpGet("user/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropertiesByUserId(int id)
        {
            var properties = await _unitOfWork.Properties.GetAll(f => f.UserId == id,
                includeProperties: "PropertyType,RentType");
            var results = _mapper.Map<IList<PropertyDTO>>(properties);
            return Ok(results);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyDTO propertyDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateProperty)}");
                return BadRequest();
            }
            var property = _mapper.Map<Property>(propertyDTO);
            await _unitOfWork.Properties.Insert(property);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetProperty", new { id = property.Id }, property);
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody] UpdatePropertyDTO propertyDTO)
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
            //map favouritePropertyDTO to favouriteProperty domain object. puts all fields values from dto to favouriteProperty object
            _mapper.Map(propertyDTO, property);
            _unitOfWork.Properties.Update(property);
            await _unitOfWork.Save();

            return NoContent();
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
    }
}
