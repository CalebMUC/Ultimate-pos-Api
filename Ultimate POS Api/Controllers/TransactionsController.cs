using Microsoft.AspNetCore.Mvc;
using Ultimate_POS_Api.DTOS;
using Ultimate_POS_Api.Services;

namespace Ultimate_POS_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IServices _service;
        public TransactionsController(IServices services)
        {
            _service = services;
        }

        [HttpPost("AddSales")]
        public async Task<IActionResult> AddSale(TransactionDto transaction) { 

            if (transaction == null) {
                return BadRequest("No DATA");
            }
            try {
                return Ok(transaction);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
