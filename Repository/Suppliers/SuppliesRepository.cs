using Microsoft.EntityFrameworkCore;
using Ultimate_POS_Api.Data;
using Ultimate_POS_Api.DTOS;
using Ultimate_POS_Api.Models;
using Newtonsoft.Json;
using System.Data;
using static Ultimate_POS_Api.DTOS.SuppliesDTOs;



namespace Ultimate_POS_Api.Repository
{

    public class SuppliesRepository : ISuppliesRepository
    {
        private readonly UltimateDBContext _dbContext;

        public SuppliesRepository(UltimateDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseStatus> AddSuplies(SuppliesDetailsDTO supplies)
        {
            using var SuppliesScope = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                foreach (SuppliesDTOs dto in supplies.Supplies)
                {
                    // Map and add the transaction
                    Supplies newSupplies = new()
                    {

                        SupplierId = dto.SupplierId,
                        SuppllyDate = dto.SuppllyDate,

                        categoriesJson = JsonConvert.SerializeObject(dto.Categories.Select(P => new
                        {  


                            CatName = P.CategoryName,
                            CatCode = P.CategoryCode,
                            CatDescription = P.CategoryDescription,
                            NoItems = P.NoOfItems,
                            StatS = P.Status,
                            CreatOn = DateTime.UtcNow,
                            UpdatOn = DateTime.UtcNow,
                            CreatBy = P.CreatedBy,
                            UpdatBy = P.UpdatedBy,

                           //  PName = P.ProductName,
                           //  PDescription = P.ProductDescription,
                           //  PType = P.ProductType,
                           //  PCategory = P.ProductCategory,
                           //  CatID = P.CategoryID,
                           //  BuyPrice = P.BuyingPrice,
                           //  //  SellingPrice = P.SellingPrice,
                           //  Qty = P.Quantity,
                           //  CrtOn = DateTime.UtcNow,
                           //  UpdtOn = DateTime.UtcNow,
                           //  CrtBy = P.CreatedBy,
                           //  UpdtBy = P.UpdatedBy,
                            // ProductCount = P.Count,

                        }).ToList()),

                    
                                  // Add the supplies to the context
                   };
                      _dbContext.Supplies.Add(newSupplies);
                           
                }
                // Save changes to the database
                await _dbContext.SaveChangesAsync();
                await SuppliesScope.CommitAsync();

                return new ResponseStatus
                {
                    Status = 200,
                    StatusMessage = "Supplies added successfully"
                };

         } catch (Exception ex)
            {

                await SuppliesScope.RollbackAsync();

                return new ResponseStatus
                {
                    Status = 500,
                    StatusMessage = $"Internal Server Error: {ex.Message}"
                };
            }
        }


        public async Task<IEnumerable<Supplies>> GetSupplies()
        {
            var response = await _dbContext.Supplies.ToListAsync();
            return response;
        }

    }

    //  public class SuppliesDetailsDTO
    //  {
    //  }
}