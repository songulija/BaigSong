using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealEstateAPI.Data;
using RealEstateAPI.IRepository;
using RealEstateAPI.ModelsDTO;
using RealEstateAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        //context for UserManager and SignInManager is ApiUser or whatever custom class you would have used
        //class that you set up in Startup. UserManager uses access to bunch of functions, that allows Signing,retriving user info, add users
        //so we dont have to write custom code for users tables. we can just inject any of those services like this. like Userroles too
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountsController> _logger;
        private readonly IAuthManager _authManager;

        public AccountsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AccountsController> logger, IAuthManager authManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _authManager = authManager;
        }
        [HttpGet]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _unitOfWork.Users.GetAll(includeProperties: "UserType");
            var results = _mapper.Map<IList<DisplayUserDTO>>(users);

            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetUser")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _unitOfWork.Users.Get(p => p.Id == id);
            var result = _mapper.Map<UserDTO>(user);
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateUser)}");
                return BadRequest();
            }
            var user = _mapper.Map<User>(userDTO);
            await _unitOfWork.Users.Insert(user);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE ettempt in {nameof(UpdateUser)}");
                return BadRequest();
            }
            var user = await _unitOfWork.Users.Get(f => f.Id == id);
            if (user == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateUser)}");
                return BadRequest("Submited invalid data");
            }
            //map favouritePropertyDTO to favouriteProperty domain object. puts all fields values from dto to favouriteProperty object
            _mapper.Map(userDTO, user);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.Save();
            return NoContent();
        }


        /// <summary>
        /// In Register we'll requiring sensitive data like passwords we dont want to send them as parameters
        /// We use POST request. when we do get request its sent accross in plain text. Get info from body
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"Registration Attempt for {userDTO.Email}");

            if (!ModelState.IsValid)
            {
                return BadRequest("Submited data is invalid");
            }
            var user = new User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
                PhoneNumber = userDTO.PhoneNumber,
                TypeId = userDTO.TypeId,
            };
            await _unitOfWork.Users.Insert(user);
            await _unitOfWork.Save();
            //return anything in 200 range. means it was succesful
            return Accepted(user);
        }

        /// <summary>
        /// POST request to api/accounts/login route. Provide LoginUserDTO object in body
        /// Checking if ModelState is valid, checking by object requirements, then validating User.
        /// If user is valid then create token and return it
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //ValidateUser returns true or false
            var validUser = await _authManager.ValidateUser(userDTO);
            if (validUser == false)
            {
                return Unauthorized();
            }
            var token = await _authManager.CreateToken();

            /*Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddSeconds(40)
            });
            */
            //return anything in 200 range. means it was succesful
            // return new object iwth an expression called Token. It'lll equal to
            // authManager method CrateToken which will return Token
            return Accepted(new { Token = token });
        }
    }
}
