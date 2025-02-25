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
    public class ProductsRepository : IproductsRepository
    {
        private readonly UltimateDBContext _dbContext;
        private readonly IConfiguration _configuration;

        public ProductsRepository(UltimateDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<Products>> GetProducts()
        {
            var response = await _dbContext.Products.ToListAsync();

            return response;
            // throw new NotImplementedException();
        }



        public async Task<ResponseStatus> AddProducts(ProductListDto productList)
        {

            var products = productList.Products.Select(dto => new Products
            {
                ProductName = dto.ProductName,
                ProductDescription = dto.ProductDescription,
                ProductType = dto.ProductType,
                ProductCategory = dto.ProductCategory,
                CategoryID = dto.CategoryID,
                BuyingPrice = dto.BuyingPrice,
                SellingPrice = dto.SellingPrice,
                Quantity = dto.Quantity,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy,
                UpdatedBy = dto.UpdatedBy,


                // Set CreatedBy, UpdatedBy based on the current user context
            }).ToList();

            // Check if product already exists
            foreach (var product in products)
            {
                    var existingProduct = _dbContext.Products.FirstOrDefault(x => x.ProductName != null && x.ProductName == product.ProductName);
                if (existingProduct != null)
                {
                    return new ResponseStatus
                    {
                        Status = 409,
                        StatusMessage = $"Product '{product.ProductName}' Already Exists"
                    };
                }
            }

            // Add the new products to the database
            _dbContext.Products.AddRange(products);

            try
            {
                // Save changes to the database asynchronously
                await _dbContext.SaveChangesAsync();

                return new ResponseStatus
                {
                    Status = 200,
                    StatusMessage = "Products Added Successfully"
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
        }



    }

} 

