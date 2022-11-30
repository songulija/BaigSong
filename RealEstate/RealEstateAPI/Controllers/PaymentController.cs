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
    public class PaymentsController : ControllerBase
    {
        // IUnitOfWork registers for every variation of GenericRepository
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PaymentsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _unitOfWork.Payments.GetAll();
            var results = _mapper.Map<IList<PaymentDTO>>(payments);
            return Ok(results);
        }
        [HttpGet("{id:int}", Name = "GetPayment")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _unitOfWork.Payments.Get(p => p.Id == id);
            var results = _mapper.Map<PaymentDTO>(payment);
            return Ok(results);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDTO paymentDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE request in {nameof(CreatePaymentDTO)}");
                return BadRequest();
            }
            var payment = _mapper.Map<Payment>(paymentDTO);
            await _unitOfWork.Payments.Insert(payment);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetPayment", new { id = payment.Id }, payment);
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] UpdatePaymentDTO paymentDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE request in {nameof(UpdatePayment)}");
                return BadRequest();
            }
            var payment = await _unitOfWork.Payments.Get(p => p.Id == id);
            if (payment == null)
            {
                _logger.LogError($"Invalid UPDATE request in {nameof(UpdatePayment)}");
                return BadRequest("Submited invalid data");
            }
            //map paymentDTO to payment domain object. put all fields values from paymentDto to payment
            _mapper.Map(paymentDTO, payment);
            _unitOfWork.Payments.Update(payment);
            await _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = _unitOfWork.Payments.Get(p => p.Id == id);
            if (payment == null)
            {
                _logger.LogError($"Invalid DELETE request in {nameof(DeletePayment)}");
                return BadRequest();
            }
            await _unitOfWork.Payments.Delete(id);
            await _unitOfWork.Save();
            return NoContent();

        }
    }
}
