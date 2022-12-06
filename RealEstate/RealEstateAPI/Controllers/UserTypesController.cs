using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class UserTypesController : ControllerBase
    {
        //context for UserManager and SignInManager is ApiUser or whatever custom class you would have used
        //class that you set up in Startup. UserManager uses access to bunch of functions, that allows Signing,retriving user info, add users
        //so we dont have to write custom code for users tables. we can just inject any of those services like this. like Userroles too
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserTypesController> _logger;

        public UserTypesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserTypesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserTypes()
        {
            var userTypes = await _unitOfWork.UserTypes.GetAll();
            var results = _mapper.Map<IList<UserTypeDTO>>(userTypes);

            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetUserType")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserType(int id)
        {
            var userType = await _unitOfWork.UserTypes.Get(p => p.Id == id);
            var result = _mapper.Map<UserTypeDTO>(userType);
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUserType([FromBody] UserTypeDTO userTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateUserType)}");
                return BadRequest();
            }
            var userType = _mapper.Map<UserType>(userTypeDTO);
            await _unitOfWork.UserTypes.Insert(userType);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetUserType", new { id = userType.Id }, userType);
        }
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUserType(int id, [FromBody] UserTypeDTO userTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE ettempt in {nameof(UpdateUserType)}");
                return BadRequest();
            }
            var userType = await _unitOfWork.UserTypes.Get(f => f.Id == id);
            if (userType == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateUserType)}");
                return BadRequest("Submited invalid data");
            }
            //map favouritePropertyDTO to favouriteProperty domain object. puts all fields values from dto to favouriteProperty object
            _mapper.Map(userTypeDTO, userType);
            _unitOfWork.UserTypes.Update(userType);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserType(int id)
        {
            var userType = await _unitOfWork.UserTypes.Get(f => f.Id == id);
            if (userType == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteUserType)}");
                return BadRequest();
            }
            await _unitOfWork.UserTypes.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
