using Microsoft.EntityFrameworkCore;
using Ultimate_POS_Api.Data;
using Ultimate_POS_Api.DTOS;
using Ultimate_POS_Api.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ultimate_POS_Api.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly UltimateDBContext _dbContext;
        private readonly IConfiguration _configuration;

        public CategoryRepository(UltimateDBContext dbContext)
        {
            _dbContext = dbContext;
        }
      

        public async Task<ResponseStatus> AddCategory(CategoryListDto JsonData)
        {


               var category = JsonData.Categ.Select(dto=>new Categories
            {
                CategoryName = dto.CategoryName,
                CategoryCode = dto.CategoryCode,
                CategoryDescription = dto.CategoryDescription,
                NoOfItems = dto.NoOfItems,
                Status = dto.Status,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy,
                UpdatedBy = dto.UpdatedBy,


            }).ToList();

            // Check if CATEGORY already exists
            foreach (var categorry in category)
            {
              var existingCategory = _dbContext.Categories.FirstOrDefault(x => x.CategoryName != null && x.CategoryName == categorry.CategoryName);
               if (existingCategory != null)
                {
                    return new ResponseStatus
                    {
                        Status = 409,
                        StatusMessage = $"Category '{categorry.CategoryName}' already exist"

                    };
                }
            }

            // Add the new category to the database
            _dbContext.Categories.AddRange(category);

            try
            {
                // Save changes to the database asynchronously
                await _dbContext.SaveChangesAsync();

                return new ResponseStatus
                {
                    Status = 200,
                    StatusMessage = "Category Added Successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseStatus
                {
                    Status = 500,
                    StatusMessage = "Internal Server Error: " + ex.Message
                };
            }
            
            // throw new NotImplementedException();
        }

        public async Task<IEnumerable<Categories>> GetCategory()
        {
             var response = await _dbContext.Categories.ToListAsync();
            return response;
        }
    }
} 
 

