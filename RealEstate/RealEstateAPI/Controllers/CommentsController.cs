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
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CommentsController> _logger;
        public CommentsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CommentsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _unitOfWork.Comments.GetAll();
            var results = _mapper.Map<List<CommentDTO>>(comments);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetComment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _unitOfWork.Comments.Get(c => c.Id == id);
            var result = _mapper.Map<CommentDTO>(comment);
            return Ok(result);
        }

        /// <summary>
        /// POST request to api/comments route. Provide CreateCommentDTO object
        /// check if model is valid. Map/convert DTO object to Comment dto object & insert it
        /// </summary>
        /// <param name="commentDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateComment)}");
                return BadRequest(ModelState);
            }
            var comment = _mapper.Map<Comment>(commentDTO);
            await _unitOfWork.Comments.Insert(comment);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetComment", new { id = comment.Id }, comment);
        }


        /// <summary>
        /// PUT request to api/comments/{id} route. Provide id & commentsDTO in body
        /// check if model valid, check if category exist with that id , map/convert dto
        /// to comments domain object for db. update and save
        /// </summary>
        /// <param name="commentDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentDTO commentDTO, int id)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateComment)}");
                return BadRequest(ModelState);
            }

            var comment = await _unitOfWork.Comments.Get(c => c.Id == id);
            if (comment == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateComment)}");
                return BadRequest("Submited invalid data");
            }
            //map/convert commentDTO into comment object. basically whatever is in commentDTO put it into brand
            _mapper.Map(commentDTO, comment);
            _unitOfWork.Comments.Update(comment);
            await _unitOfWork.Save();
            return NoContent();
        }


        /// <summary>
        /// DELETE request to api/comments route. Check if record exist. Delete and save it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _unitOfWork.Comments.Get(c => c.Id == id);
            if (comment == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteComment)}");
                return BadRequest("Submited data is invalid");
            }

            await _unitOfWork.Comments.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
