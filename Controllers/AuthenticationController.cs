using Microsoft.AspNetCore.Mvc;
using Ultimate_POS_Api.DTOS;
using Ultimate_POS_Api.Services;

namespace Ultimate_POS_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase

    {
        private readonly IServices _service;
        public AuthenticationController(IServices services)
        {
            _service = services;
        }

         [HttpPost("Login")]
        public async Task<ActionResult> Login(UserInfo userInfo) {

            try
            {
                var response = await _service.Login(userInfo);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(Register register)
        {
            try
            {
                var response = await _service.Register(register);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
