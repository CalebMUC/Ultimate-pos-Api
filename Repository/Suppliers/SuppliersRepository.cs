using Microsoft.EntityFrameworkCore;
using Ultimate_POS_Api.Data;
using Ultimate_POS_Api.DTOS;
using Ultimate_POS_Api.Models;
// using System;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Security.Cryptography;
// using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Data;
// using Microsoft.Extensions.Configuration;
// using Microsoft.IdentityModel.Tokens;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
using Newtonsoft.Json;

namespace Ultimate_POS_Api.Repository
{

    public class SuppliersRepository : ISuppliersRepository
    {

        private readonly UltimateDBContext _dbContext;

        public SuppliersRepository(UltimateDBContext dbContext)
        {
            _dbContext = dbContext;
            // _jwtSettings = jwtsettings.Value;
        }


        public async Task<ResponseStatus> AddSuplier(SuppliersDetilsDTO supplier)
        {
            // using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transactionScope = await _dbContext.Database.BeginTransactionAsync();

            using var SuppliersScope = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // Iterate through each transaction in the list
                foreach (SuppliersDTO dto in supplier.Supplier)
                {
                    // Map and add the transaction
                    Supplier newSupplier = new()
                    {
                        SupplierId = dto.SupplierId,
                        SupplierName = dto.SupplierName,
                        SupplierType = dto.SupplierType,
                        Industry = dto.Industry,
                        KRAPIN = dto.KRAPIN,
                        BusinessLicenseNumber = dto.BusinessLicenseNumber,
                        SupplierStatus = dto.SupplierStatus,
                        Remarks = dto.Remarks,
                        CreatedBy = dto.CreatedBy,
                        UpdatedBy = dto.CreatedBy,
                        CreatedOn = DateTime.UtcNow,


                        ContactDetailsJson = JsonConvert.SerializeObject(dto.Contactdetails.Select(S => new
                        {
                            SupplierName = S.Name,
                            SupplierEmail = S.Email,
                            SuplierPhone = S.Phone
                        }).ToList()),

                        AddressDetailsJson = JsonConvert.SerializeObject(dto.AddressDetails.Select(A => new
                        {
                            AddressLocationName = A.LocationName,
                            AddressTown = A.Town,
                            AddressPostal = A.Postal,

                        }).ToList()),


                        ProductInfosJson = JsonConvert.SerializeObject(dto.Productsdetails.Select(P => new
                        {

                            ProductNamee = P.ProductName,
                            ProductCategory = P.Category,
                            ProductUnitMeasure = P.UnitMeasure,
                            // ProductCount = P.Count,

                        }).ToList()),


                        ContractDetailsJson = JsonConvert.SerializeObject(dto.ContractDetails.Select(C => new
                        {

                            ContStartDate = C.ContractStartDate,
                            ContEndDate = C.ContractEndDate,
                            ContTerms = C.Terms,

                        }).ToList()),  

                        BankDetailsJson = JsonConvert.SerializeObject(dto.BankAccountDetails.Select(B => new 
                        {
                            B.BankName,
                            B.AccountNumber

                        }).ToList() ),

                        MpesaDetailsJson = JsonConvert.SerializeObject(dto.MpesaDetails.Select(M => new 
                        {
                            M.Till,
                            M.Pochi

                        }).ToList()),




                    };

             

                    // Check if Supplier already exists
                    foreach (var sup in supplier.Supplier)
                    {
                        var existingSupplier = _dbContext.Supplier.FirstOrDefault(x => x.SupplierName != null && x.SupplierName == sup.SupplierName);
                        if (existingSupplier != null)
                        {
                            return new ResponseStatus
                            {
                                Status = 409,
                                StatusMessage = $"Supplier '{sup.SupplierName}' already exist"
                            };
                        }
                        else
                        {

                            // Add the supplier to the context
                            _dbContext.Supplier.Add(newSupplier);
                        }

                    }




                }

                // Save changes to the database
                await _dbContext.SaveChangesAsync();
                await SuppliersScope.CommitAsync();

                return new ResponseStatus
                {
                    Status = 200,
                    StatusMessage = "Supplier added successfully"
                };
            }
            catch (Exception ex)
            {
                await SuppliersScope.RollbackAsync();

                return new ResponseStatus
                {
                    Status = 500,
                    StatusMessage = $"Internal Server Error: {ex.Message}"
                };
            }
        }

        public async Task<IEnumerable<Supplier>> GetSupplier()
        {
            var response = await _dbContext.Supplier.ToListAsync();
            return response;
        }

    }


}


