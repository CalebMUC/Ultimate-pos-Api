using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Ultimate_POS_Api.DTOS;
using Ultimate_POS_Api.Models;
using Ultimate_POS_Api.Repository;
using Ultimate_POS_Api.Services;

namespace Ultimate_POS_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpPost("AddCategory")]
        public async Task<ActionResult> AddCategory(CategoryListDto category)
        {

            //var jsonData = JsonConvert.SerializeObject(products);

            try
            {
                var response = await _categoryRepository.AddCategory(category);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPost("GetCategory")]
        public async Task<ActionResult> GetCategory()
        {


            try
            {

                var response = await _categoryRepository.GetCategory();

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
