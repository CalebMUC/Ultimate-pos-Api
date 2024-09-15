using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Ultimate_POS_Api.DTOS;
using Ultimate_POS_Api.Models;
using Ultimate_POS_Api.Services;

namespace Ultimate_POS_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UltimateController : ControllerBase
    {
        private readonly IServices _service;
        public UltimateController(IServices services) { 
            _service = services;
        }
        [HttpPost("AddProducts")]
        public async  Task<ActionResult> AddProducts(ProductListDto products) {

            //var jsonData = JsonConvert.SerializeObject(products);

            try
            {
                var response = await _service.AddProducts(products);
                return Ok(response);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("GetProducts")]
        public async Task<ActionResult> GetProducts()
        {


            try {

                var response = await _service.GetProducts();
          
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
