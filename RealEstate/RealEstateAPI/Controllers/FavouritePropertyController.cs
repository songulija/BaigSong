using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealEstateAPI.Data;
using RealEstateAPI.IRepository;
using RealEstateAPI.ModelsDTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritePropertyController : ControllerBase
    {
        // IUnitOfWork registers for every variation of GenericRepository
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<FavouritePropertyController> _logger;
        private readonly DatabaseContext _databaseContext;

        public FavouritePropertyController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FavouritePropertyController> logger, DatabaseContext databaseContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _databaseContext = databaseContext;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFavouriteProperties()
        {
            var favouriteProperties = await _unitOfWork.FavouriteProperties.GetAll(includeProperties: "Property");
            var results = _mapper.Map<IList<FavouriteProperty>>(favouriteProperties);
            return Ok(results);
        }
        [HttpGet("{id:int}", Name = "GetFavouriteProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFavouriteProperty(int id)
        {
            var favouriteProperty = await _unitOfWork.FavouriteProperties.Get(f => f.Id == id, includeProperties: "Property,User");
            var result = _mapper.Map<FavouritePropertyDTO>(favouriteProperty);
            return Ok(result);
        }
        [HttpGet("property/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLikesCountByPropertyId(int id)
        {
            var favouriteProperties = await _unitOfWork.FavouriteProperties.GetAll(f => f.PropertyId == id, includeProperties: "Property,User");
            var results = _mapper.Map<IList<FavouritePropertyDTO>>(favouriteProperties);
            return Ok(results.Count);
        }

        [HttpGet("user/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFavouritePropertiesByUserId(int id)
        {
            var favouriteProperties = await _unitOfWork.FavouriteProperties.GetAll(f => f.UserId == id,
                includeProperties: "Property,User");
            var results = _mapper.Map<IList<FavouritePropertyDTO>>(favouriteProperties);
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateFavouriteProperty([FromBody] CreateFavouritePropertyDTO favouritePropertyDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateFavouriteProperty)}");
                return BadRequest();
            }
            var favouriteProperty = _mapper.Map<FavouriteProperty>(favouritePropertyDTO);
            await _unitOfWork.FavouriteProperties.Insert(favouriteProperty);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetFavouriteProperty", new { id = favouriteProperty.Id }, favouriteProperty);

        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateFavouriteProperty(int id, [FromBody] UpdateFavouritePropertyDTO favouritePropertyDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE ettempt in {nameof(UpdateFavouriteProperty)}");
                return BadRequest();
            }
            var favouriteProperty = await _unitOfWork.FavouriteProperties.Get(f => f.Id == id);
            if (favouriteProperty == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateFavouriteProperty)}");
                return BadRequest("Submited invalid data");
            }
            //map favouritePropertyDTO to favouriteProperty domain object. puts all fields values from dto to favouriteProperty object
            _mapper.Map(favouritePropertyDTO, favouriteProperty);
            _unitOfWork.FavouriteProperties.Update(favouriteProperty);
            await _unitOfWork.Save();

            return NoContent();
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFavouriteProperty(int id)
        {
            var favouriteProperty = await _unitOfWork.FavouriteProperties.Get(f => f.Id == id);
            if (favouriteProperty == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteFavouriteProperty)}");
                return BadRequest();
            }
            await _unitOfWork.FavouriteProperties.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }


    }

}
