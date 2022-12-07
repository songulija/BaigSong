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
    public class PropertyTypesController : ControllerBase
    {
        // IUnitOfWork registers for every variation of GenericRepository
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PropertyTypesController> _logger;

        public PropertyTypesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PropertyTypesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropertyTypes()
        {
            var propertyTypes = await _unitOfWork.PropertyTypes.GetAll();
            var results = _mapper.Map<IList<PropertyTypeDTO>>(propertyTypes);
            return Ok(results);
        }
        [HttpGet("{id:int}", Name = "GetPropertyType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropertyType(int id)
        {
            var propertyType = await _unitOfWork.PropertyTypes.Get(p => p.Id == id);
            var result = _mapper.Map<PropertyDTO>(propertyType);
            return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePropertyType([FromBody] CreatePropertyTypeDTO propertyTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreatePropertyType)}");
                return BadRequest();
            }
            var propertyType = _mapper.Map<PropertyType>(propertyTypeDTO);
            await _unitOfWork.PropertyTypes.Insert(propertyType);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetPropertyType", new { id = propertyType.Id }, propertyType);
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePropertyType(int id, [FromBody] UpdatePropertyTypeDTO propertyTypeDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE ettempt in {nameof(UpdatePropertyType)}");
                return BadRequest();
            }
            var propertyType = await _unitOfWork.PropertyTypes.Get(f => f.Id == id);
            if (propertyType == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdatePropertyType)}");
                return BadRequest("Submited invalid data");
            }
            //map favouritePropertyDTO to favouriteProperty domain object. puts all fields values from dto to favouriteProperty object
            _mapper.Map(propertyTypeDTO, propertyType);
            _unitOfWork.PropertyTypes.Update(propertyType);
            await _unitOfWork.Save();

            return NoContent();
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePropertyType(int id)
        {
            var propertyType = await _unitOfWork.PropertyTypes.Get(f => f.Id == id);
            if (propertyType == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeletePropertyType)}");
                return BadRequest();
            }
            await _unitOfWork.PropertyTypes.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
