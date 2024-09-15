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
    public class UltimateRepository : IRepository
    {
        private readonly UltimateDBContext _dbContext;
        private readonly IConfiguration _configuration;

        public UltimateRepository(UltimateDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Products>> GetProducts()
        {

            var response = await _dbContext.Products.ToListAsync();

            return response;

        }

        public async Task<ResponseStatus> Register(Register register)
        {
            byte[] salt;

            try
            {
                //check if user already exists
                var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == register.UserName ||
                u.Email == register.Email);
                if (existingUser != null)
                {
                     return new ResponseStatus
                    {
                        Status = 400,
                        StatusMessage = "User Already Exists"
                    };

                }
                // Get role details and specify access level
                var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleName == register.Role);
                if (role == null)
                {
                    role = new Role
                    {
                        RoleName = register.Role,
                        AccessLevel = GetAccessLevel(register.Role),
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow,
                        CreatedBy = "System",
                        UpdatedBy = "System"
                    };
                    _dbContext.Roles.Add(role);
                    await _dbContext.SaveChangesAsync();
                }

                // Encrypt Password

                var hashedPassword = HashPassword(register.Password, out salt);

                // Create the user
                var newUser = new User
                {
                    UserName = register.UserName,
                    Email = register.Email,
                    Password = hashedPassword,
                    salt = Convert.ToBase64String(salt),
                    RoleId = role.RoleId , 
                    IsLoggedIn = 0,
                    TimeLoggedIn = DateTime.MinValue,
                    IsLoggedOut = 0,
                    TimeLoggedOut = DateTime.MinValue,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedBy = register.OperatorID,
                    UpdatedBy = register.OperatorID,
           
                };

                //insert users 

                _dbContext.Users.Add(newUser);

                //save changes

                await _dbContext.SaveChangesAsync();



                return new ResponseStatus
                {
                    Status = 200,
                    StatusMessage = "User Created SuccessFully"
                };

                



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<LoginResponseStatus> Login(UserInfo userInfo)
        {
            string token = "";

            try
            {
                // Fetch user details based on the username
                var user = await _dbContext.Users
                                           .Where(u => u.UserName == userInfo.UserName)
                                           .FirstOrDefaultAsync();

                // Check if user exists
                if (user == null)
                {
                    return new LoginResponseStatus
                    {
                        Status = 400,
                        StatusMessage = "User doesn't exist"
                    };
                }

                // Access stored password hash and salt
                string storedHash = user.Password;
                byte[] storedSalt = Convert.FromBase64String(user.salt);

                // Verify the password
                bool isPasswordValid = VerifyPassword(userInfo.Password, storedHash, storedSalt);

                if (isPasswordValid)
                {
                    // Generate JWT Token
                    token = GenerateJwtToken(user);

                    // Update IsLoggedIn and TimeLoggedIn
                    user.IsLoggedIn = 1;  // Mark the user as logged in
                    user.TimeLoggedIn = DateTime.Now;  // Set the current time as login time

                    // Save the changes to the database
                    _dbContext.Users.Update(user);
                    await _dbContext.SaveChangesAsync();

                    // Return successful login response
                    return new LoginResponseStatus
                    {
                        Status = 200,
                        StatusMessage = "Login successful",
                        Token = token,
                        UserRole = user.RoleId.ToString()
                    };
                }
                else
                {
                    // Return invalid password response
                    return new LoginResponseStatus
                    {
                        Status = 401,
                        StatusMessage = "Invalid password"
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during login: " + ex.Message);
            }
        }



        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            // Get the key, issuer, audience, and expiration time from configuration
            var secretKey = jwtSettings["Secret"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expirationInMinutes = int.Parse(jwtSettings["ExpirationInMinutes"]);

            // Create security key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create claims for the token (like UserId, Role, etc.)
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique ID for the token
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), // User ID claim
            new Claim(ClaimTypes.Role, user.RoleId.ToString() )// Role claim
        };

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationInMinutes),
                signingCredentials: credentials);

            // Serialize the token to string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string HashPassword(string password, out byte[] salt) { 

            salt = new byte[128/8];

            try {
                //Generate a random number and generate bytea
                using (var rng = RandomNumberGenerator.Create()) { 
                    rng.GetBytes(salt);
                }

                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                return hashed;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }

        }

        private int GetAccessLevel(string roleName)
        {
            return roleName switch
            {
                "Admin" => 10,
                "User" => 5,
                "Guest" => 1,
                _ => 0 // Default access level for unknown roles
            };
        }


        private bool VerifyPassword(string enteredPassword, string storedHash, byte[] storedSalt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: storedSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed == storedHash;
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
