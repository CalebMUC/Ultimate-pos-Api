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
using Newtonsoft.Json;

namespace Ultimate_POS_Api.Repository
{
   
    public class TransactionRepository : ItransactionRepository {

         private readonly UltimateDBContext _dbContext;
           public TransactionRepository(UltimateDBContext dbContext)
        {
            _dbContext = dbContext;
            // _jwtSettings = jwtsettings.Value;
        }
       
       
        public async Task<ResponseStatus> AddSale(TransactionListDto transaction)
        {
            using var transactionScope = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                // Iterate through each transaction in the list
                foreach (var dto in transaction.Transactions)
                {
                    // Insert new PaymentDetails records for audit purposes, one per payment method used
                    foreach (var paymentDetailDto in dto.PaymentDetails)
                    {
                        var newPayment = new PaymentDetails
                        {
                            PaymentID = paymentDetailDto.PaymentID,
                            PaymentReference = paymentDetailDto.PaymentReference,
                            Amount = paymentDetailDto.Amount,
                            PaymentDate = DateTime.UtcNow // Always use the current date for audits
                        };
                        _dbContext.PaymentDetails.Add(newPayment); // Insert new record for audit
                    }

                    // Map and add the transaction
                    var newTransaction = new Transactions
                    {
                        TotalCost = dto.TotalCost,
                        TotalDiscount = dto.TotalDiscount,
                        AmountRecieved = dto.AmountRecieved,
                        CashChange = dto.CashChange,
                        Quantity = dto.Quantity,
                        Status = dto.Status,
                        StatusMessage = dto.StatusMessage,
                        //PaymentMethodID = dto.PaymentDetails.FirstOrDefault().PaymentMethodID, // Use first payment method
                        PaymentConfirmation = dto.PaymentConfirmation,
                        TransactionDateDate = DateTime.UtcNow,
                        CreatedBy = dto.CreatedBy,
                        UpdatedBy = dto.CreatedBy,
                        CreatedOn = DateTime.UtcNow,
                        UserID = dto.UserID,
                        ProductsJson = JsonConvert.SerializeObject(dto.products.Select(p => new
                        {
                            ProductName = p.ProductName,
                            ProductID = p.ProductID,
                            Quantity = p.Quantity,
                            Price = p.Price,
                            Discount = p.Discount
                        }).ToList()),

                        // Map the TransactionProducts
                        TransactionProducts = dto.products.Select(productDto => new TransactionProduct
                        {
                            ProductID = productDto.ProductID,
                            Quantity = productDto.Quantity,
                        }).ToList()
                    };

                    // Update the product quantities
                    foreach (var product in dto.products)
                    {
                        var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductID == product.ProductID);
                        if (existingProduct != null)
                        {
                            // Reduce product quantity
                            existingProduct.Quantity -= product.Quantity;

                            // Prevent negative quantity
                            if (existingProduct.Quantity < 0)
                            {
                                return new ResponseStatus
                                {
                                    Status = 400,
                                    StatusMessage = $"Insufficient stock for product: {existingProduct.ProductName}"
                                };
                            }
                            _dbContext.Products.Update(existingProduct);
                        }
                    }

                    // Add the transaction to the context
                    _dbContext.Transactions.Add(newTransaction);
                }

                // Save changes to the database
                await _dbContext.SaveChangesAsync();
                await transactionScope.CommitAsync();

                return new ResponseStatus
                {
                    Status = 200,
                    StatusMessage = "Transaction completed successfully"
                };
            }
            catch (Exception ex)
            {
                await transactionScope.RollbackAsync();

                return new ResponseStatus
                {
                    Status = 500,
                    StatusMessage = $"Internal Server Error: {ex.Message}"
                };
            }
        }
       
    }
}


    