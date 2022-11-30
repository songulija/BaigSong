using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RealEstateAPI.Data;
using RealEstateAPI.IRepository;
using RealEstateAPI.ModelsDTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        //context for UserManager and SignInManager is ApiUser or whatever custom class you would have used
        //class that you set up in Startup. UserManager uses access to bunch of functions, that allows Signing,retriving user info, add users
        //so we dont have to write custom code for users tables. we can just inject any of those services like this. like Userroles too
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ImagesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        /*[HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetImages()
        {
            var images = await _unitOfWork.Images.GetAll();
            foreach(var image in images)
            {
                image.Photo = GetImage(Convert.ToBase64String(image.Photo));
            }
            var results = _mapper.Map<IList<ImageDTO>>(images);

            return Ok(results);
        }*/

        /*[HttpGet("property/{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProperty(int id)
        {
            var images = await _unitOfWork.Images.GetAll(x => x.PropertyId == id);
            foreach (var image in images)
            {
                image.Photo = GetImage(Convert.ToBase64String(image.Photo));
            }
            var results = _mapper.Map<IList<ImageDTO>>(images);

            return Ok(results);
        }*/

        /*[HttpGet("{id:int}", Name = "GetImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _unitOfWork.Images.Get(p => p.Id == id);
            image.Photo = GetImage(Convert.ToBase64String(image.Photo));
            var result = _mapper.Map<ImageDTO>(image);
            return Ok(result);
        }*/

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateImage([FromBody] ImageDTO imageDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateImage)}");
                return BadRequest();
            }
            var image = _mapper.Map<Image>(imageDTO);
            await _unitOfWork.Images.Insert(image);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetImage", new { id = image.Id }, image);
        }

        /*[HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> SaveFile(FileUpload fileObj)
        {
            Image oImage = JsonConvert.DeserializeObject<Image>(fileObj.Image);
            if (fileObj.file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await fileObj.file.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    //Student photo in byte[] format
                    oImage.Photo = fileBytes;
                    await _unitOfWork.Images.Insert(oImage);
                    await _unitOfWork.Save();
                    if (oImage.Id > 0)
                    {
                        return Ok();
                    }
                }
            }
            _logger.LogError($"Invalid Upload Image attempt in {nameof(SaveFile)}");
            return BadRequest("Submited invalid data");
        }*/

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateImage(int id, [FromBody] ImageDTO imageDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE ettempt in {nameof(UpdateImage)}");
                return BadRequest();
            }
            var image = await _unitOfWork.Images.Get(f => f.Id == id);
            if (image == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateImage)}");
                return BadRequest("Submited invalid data");
            }
            //map favouritePropertyDTO to favouriteProperty domain object. puts all fields values from dto to favouriteProperty object
            _mapper.Map(imageDTO, image);
            _unitOfWork.Images.Update(image);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _unitOfWork.Images.Get(f => f.Id == id);
            if (image == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteImage)}");
                return BadRequest();
            }
            await _unitOfWork.Images.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }

        public byte[] GetImage(string sBase64String)
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
