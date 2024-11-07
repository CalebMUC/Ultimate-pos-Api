using Microsoft.AspNetCore.Mvc;
using Ultimate_POS_Api.DTOS;
using Ultimate_POS_Api.Services;
using Ultimate_POS_Api.Repository;

namespace Ultimate_POS_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ItransactionRepository _transactionrepository;
        public TransactionsController(ItransactionRepository transaction)
        {
            _transactionrepository = transaction;
        }

        [HttpPost("AddSales")]
        public async Task<IActionResult> AddSale(TransactionListDto transaction)  { 

            if (transaction == null) {
                return BadRequest("No DATA");
            }
            try {
                // var response = await _transactionrepository.AddSale(transaction);
                 var response = await _transactionrepository.AddSale(transaction);
                return Ok(response);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
