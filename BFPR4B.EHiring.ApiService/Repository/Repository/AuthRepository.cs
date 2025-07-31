using BFPR4B.EHiring.ApiService.Helpers.Response;
using BFPR4B.EHiring.ApiService.Helpers.Settings;
using BFPR4B.EHiring.ApiService.Helpers.Utility;
using BFPR4B.EHiring.ApiService.Models.Data;
using BFPR4B.EHiring.ApiService.Models.DTO;
using BFPR4B.EHiring.ApiService.Models.Model;
using BFPR4B.EHiring.ApiService.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace BFPR4B.EHiring.ApiService.Repository.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        private string _secretKey;
        private string _Audience;
        private string _Issuer;
        private readonly string ApplicationprofileUploadFolder;
        private readonly string ApplicationprofileUploadURL;

        public AuthRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
            _secretKey = configuration.GetValue<string>("APISettings:SecretKey");
            _Audience = configuration.GetValue<string>("APISettings:Audience");
            _Issuer = configuration.GetValue<string>("APISettings:Issuer");
            ApplicationprofileUploadFolder = configuration.GetValue<string>("AppSettings:ProfileUploadFolder");
            ApplicationprofileUploadURL = configuration.GetValue<string>("AppSettings:ProfileUploadURL");
        }

        private string GenerateTempToken(Tblapplicationuser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["APISettings:SecretKey"]; // Get from appsettings.json
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("APISettings:SecretKey is not configured.");
            }
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new List<Claim>
            {
                new Claim("Userno", user.Userno.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Userno.ToString()),
                new Claim("Username", user.Emailaddress ?? user.Contactno ?? ""),
                new Claim(ClaimTypes.Email, user.Emailaddress ?? ""),
                new Claim(ClaimTypes.MobilePhone, user.Contactno ?? ""),
                new Claim(ClaimTypes.Name, user.Firstname + " " + user.Lastname),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("is_temp_auth", "true") // Custom claim to mark as temporary
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(5), // Temp token valid for 5 minutes
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["APISettings:Issuer"],
                Audience = _configuration["APISettings:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Helper to generate the final access token (full JWT)
        private string GenerateAccessToken(Tblapplicationuser user, int expiresInMinutes = 60) // Example expiry: 60 minutes
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["APISettings:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("APISettings:SecretKey is not configured.");
            }
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new List<Claim>
            {
                new Claim("Userno", user.Userno.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Userno.ToString()),
                new Claim("Username", user.Emailaddress ?? user.Contactno ?? ""),
                new Claim(ClaimTypes.Email, user.Emailaddress ?? ""),
                new Claim(ClaimTypes.MobilePhone, user.Contactno ?? ""),
                new Claim(ClaimTypes.Name, user.Firstname + " " + user.Lastname),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiresInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["APISettings:Issuer"],
                Audience = _configuration["APISettings:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<ApiResponse> LoginOTPAsync(LoginRequestDTO parameters)
        {
            var strategy = _db.Database.CreateExecutionStrategy(); // Create retrying execution strategy

            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _db.Database.BeginTransactionAsync();

                try
                {
                    // ✅ Validate fields
                    if (string.IsNullOrEmpty(parameters.Client_id) ||
                        string.IsNullOrEmpty(parameters.Client_secret) ||
                        string.IsNullOrEmpty(parameters.Username) ||
                        string.IsNullOrEmpty(parameters.Userpass) ||
                        string.IsNullOrEmpty(parameters.Grant_type) ||
                        string.IsNullOrEmpty(parameters.Login_type))
                    {
                        return new ApiResponse
                        {
                            StatusCode = HttpStatusCode.Conflict,
                            IsSuccess = false,
                            ErrorMessages = "Login unsuccessful. Please review your login details and try again."
                        };
                    }

                    // ✅ Check credentials
                    if (parameters.Client_id != Settings.Client_ID ||
                        parameters.Client_secret != Settings.Client_Secret ||
                        parameters.Grant_type != Settings.Grant_Type ||
                        parameters.Login_type != Settings.Login_Type)
                    {
                        return new ApiResponse
                        {
                            StatusCode = HttpStatusCode.Conflict,
                            IsSuccess = false,
                            ErrorMessages = "Login unsuccessful. Please review your login details and try again."
                        };
                    }

                    var user = await _db.Tblapplicationusers.FirstOrDefaultAsync(i => i.Emailaddress.Trim() == parameters.Username.Trim() || i.Contactno.Trim() == parameters.Username.Trim());

                    if (user == null)
                    {
                        return new ApiResponse
                        {
                            StatusCode = HttpStatusCode.Conflict,
                            IsSuccess = false,
                            ErrorMessages = "No matching account was found. Please review your login details and try again."
                        };
                    }

                    bool isPasswordValid = PasswordHelper.VerifyPassword(parameters.Userpass, user.Userpass);
                    if (!isPasswordValid)
                    {
                        return new ApiResponse
                        {
                            StatusCode = HttpStatusCode.Conflict,
                            IsSuccess = false,
                            ErrorMessages = "Invalid username or password. Please review your login details and try again."
                        };
                    }

                    int loginUserno = user.Userno;

                    string otpCode = new Random().Next(100000, 999999).ToString();

                    Tblotp newOtp = new Tblotp
                    {
                        Userno = loginUserno,
                        Otpcode = otpCode,
                        Otptype = "LOGIN",
                        Expiration = DateTime.Now.AddMinutes(5),
                        Channel = "EMAIL",
                        System = "EHIRING",
                        Dateencoded = DateTime.Now,
                    };

                    await _db.Tblotps.AddAsync(newOtp);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();

                    string tempToken = GenerateTempToken(user);

                    return new ApiResponse
                    {
                        StatusCode = HttpStatusCode.OK,
                        IsSuccess = true,
                        ErrorMessages = "",
                        Data = tempToken
                    };
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<ApiResponse> LoginAsync(LoginRequestDTO parameters)
        {

            if (parameters.Client_id == null || parameters.Client_secret == null || parameters.Username == null
                || parameters.Userpass == null || parameters.Grant_type == null || parameters.Login_type == null)
            {
                return new ApiResponse()
                {
                    StatusCode = HttpStatusCode.Conflict,
                    IsSuccess = false,
                    ErrorMessages = "Invalid Login."
                };
            }

            if (parameters.Client_id != null)
            {
                if (parameters.Client_id != Settings.Client_ID)
                {
                    return new ApiResponse()
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        IsSuccess = false,
                        ErrorMessages = "Invalid Login."
                    };
                }
            }

            if (parameters.Client_secret != null)
            {
                if (parameters.Client_secret != Settings.Client_Secret)
                {
                    return new ApiResponse()
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        IsSuccess = false,
                        ErrorMessages = "Invalid Login."
                    };
                }
            }

            if (parameters.Grant_type != null)
            {
                if (parameters.Grant_type != Settings.Grant_Type)
                {
                    return new ApiResponse()
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        IsSuccess = false,
                        ErrorMessages = "Invalid Login."
                    };
                }
            }

            if (parameters.Login_type != null)
            {
                if (parameters.Login_type != Settings.Login_Type)
                {
                    return new ApiResponse()
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        IsSuccess = false,
                        ErrorMessages = "Invalid Login."
                    };
                }
            }

            var _user = await _db.Tblapplicationusers.FirstOrDefaultAsync(i => i.Emailaddress == parameters.Username.Trim() || i.Contactno == parameters.Username.Trim());

            if (_user == null)
            {
                return new ApiResponse()
                {
                    StatusCode = HttpStatusCode.Conflict,
                    IsSuccess = false,
                    ErrorMessages = "Invalid email address/contact number not found."
                };
            }

            if (_user != null)
            {
                bool isPasswordValid = PasswordHelper.VerifyPassword(parameters.Userpass, _user.Userpass);

                if (!isPasswordValid)
                {
                    return new ApiResponse()
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        IsSuccess = false,
                        ErrorMessages = "Invalid username and password."
                    };
                }
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _Audience,
                Issuer = _Issuer,
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("userno", _user.Userno.ToString()),
                        new Claim(ClaimTypes.Email, _user.Emailaddress),
                        new Claim(ClaimTypes.MobilePhone, _user.Contactno),
                        new Claim(ClaimTypes.Name, _user.Firstname + " " + _user.Lastname),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Retrieve the expiry time
            var tokenExpiry = tokenDescriptor.Expires;

            var param = new SqlParameter[]
            {
                    new SqlParameter("@userno", _user.Userno)
            };

            ApplicationUserModel result = _db.GetApplicationUserDetails.FromSqlRaw("EXEC sp_getapplicationuserdetail @userno", param).AsNoTracking().AsEnumerable().FirstOrDefault();

            if (result == null)
            {
                return new ApiResponse()
                {
                    StatusCode = HttpStatusCode.Conflict,
                    IsSuccess = false,
                    ErrorMessages = "Invalid username and password."
                };
            }

            // Combine the base directory with the filename
            var _localurl = Path.Combine(ApplicationprofileUploadFolder, _user.Userno.ToString().Trim() + "." + _user.Profilepictype.Trim());
            var _publicurl = Path.Combine(ApplicationprofileUploadURL, _user.Userno.ToString().Trim() + "." + _user.Profilepictype.Trim());

            _user.Profileurl = Utils.CheckFileURL2(_publicurl, _localurl);

            var login = new LoginResponseModel
            {
                Expiration = Convert.ToDateTime(tokenExpiry),
                Data = _user,
                AccessToken = tokenHandler.WriteToken(token)
            };

            return new ApiResponse
            {

                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                ErrorMessages = "",
                Data = result,
            };
        }

        public async Task<ApiResponse> LoginV2Async(LoginRequestDTO parameters)
        {

            if (parameters.Client_id == null || parameters.Client_secret == null || parameters.TempToken == null
                || parameters.Grant_type == null || parameters.Login_type == null)
            {
                return new ApiResponse()
                {
                    StatusCode = HttpStatusCode.Conflict,
                    IsSuccess = false,
                    ErrorMessages = "Invalid Login."
                };
            }

            if (string.IsNullOrEmpty(parameters.TempToken)) 
            {
                return new ApiResponse()
                {
                    StatusCode = HttpStatusCode.Conflict,
                    IsSuccess = false,
                    ErrorMessages = "Invalid Login."
                };
            }

            if (parameters.Client_id != null)
            {
                if (parameters.Client_id != Settings.Client_ID)
                {
                    return new ApiResponse()
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        IsSuccess = false,
                        ErrorMessages = "Invalid Login."
                    };
                }
            }

            if (parameters.Client_secret != null)
            {
                if (parameters.Client_secret != Settings.Client_Secret)
                {
                    return new ApiResponse()
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        IsSuccess = false,
                        ErrorMessages = "Invalid Login."
                    };
                }
            }

            if (parameters.Grant_type != null)
            {
                if (parameters.Grant_type != Settings.Grant_Type)
                {
                    return new ApiResponse()
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        IsSuccess = false,
                        ErrorMessages = "Invalid Login."
                    };
                }
            }

            if (parameters.Login_type != null)
            {
                if (parameters.Login_type != Settings.Login_Type)
                {
                    return new ApiResponse()
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        IsSuccess = false,
                        ErrorMessages = "Invalid Login."
                    };
                }
            }

            var _user = await _db.Tblapplicationusers.FirstOrDefaultAsync(i => i.Emailaddress == parameters.Username.Trim() || i.Contactno == parameters.Username.Trim());

            if (_user == null)
            {
                return new ApiResponse()
                {
                    StatusCode = HttpStatusCode.Conflict,
                    IsSuccess = false,
                    ErrorMessages = "Invalid email address/contact number not found."
                };
            }

            if (_user != null)
            {
                bool isPasswordValid = PasswordHelper.VerifyPassword(parameters.Userpass, _user.Userpass);

                if (!isPasswordValid)
                {
                    return new ApiResponse()
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        IsSuccess = false,
                        ErrorMessages = "Invalid username and password."
                    };
                }
            }


            // 1. Validate the TempToken
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["APISettings:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("APISettings:SecretKey is not configured.");
            }
            var key = Encoding.ASCII.GetBytes(secretKey);

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["APISettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["APISettings:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero // Crucial for strict temp token expiry
            };

            ClaimsPrincipal principal;
            try
            {
                principal = tokenHandler.ValidateToken(parameters.TempToken, validationParameters, out SecurityToken validatedToken);
            }
            catch (SecurityTokenExpiredException)
            {
                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    IsSuccess = false,
                    ErrorMessages = Settings.INVALID_SESSION_ERR_MSG
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    IsSuccess = false,
                    ErrorMessages = Settings.INVALID_SESSION_ERR_MSG
                };
            }

            // Verify it's indeed a temporary token
            if (!principal.HasClaim(c => c.Type == "is_temp_auth" && c.Value == "true"))
            {
                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.Forbidden, // Or Unauthorized
                    IsSuccess = false,
                    ErrorMessages = "Invalid temporary token type. Access denied."
                };
            }

            var userNoClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (userNoClaim == null || !int.TryParse(userNoClaim.Value, out int userNoFromToken))
            {
                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    IsSuccess = false,
                    ErrorMessages = "User information missing from temporary token."
                };
            }

            var user = await _db.Tblapplicationusers.FirstOrDefaultAsync(u => u.Userno == userNoFromToken);
            if (user == null)
            {
                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    IsSuccess = false,
                    ErrorMessages = "User associated with this session not found."
                };
            }



            // 2. Validate the OTP
            var latestOtp = await _db.Tblotps
                .Where(o => o.Userno == user.Userno && o.Otptype == "LOGIN" && o.Expiration > DateTime.Now && o.Otpcode.Trim() == parameters.Otpcode.Trim() & o.System.Trim().ToUpper() == "EHIRING") 
                .OrderByDescending(o => o.Dateencoded)
                .FirstOrDefaultAsync();

            if (latestOtp == null) // OTP either wrong or expired
            {
                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    IsSuccess = false,
                    ErrorMessages = "Invalid or expired OTP. Please try again."
                };
            }

            // Mark OTP as used to prevent replay attacks
            latestOtp.IsUsed = true; // Set the IsUsed property to true
            await _db.SaveChangesAsync();

            // 3. Generate the final access token
            var accessToken = GenerateAccessToken(user); // Default expiry 60 mins
            int expiresIn = 60 * 60; // 60 minutes * 60 seconds


            var param = new SqlParameter[]
            {
                    new SqlParameter("@userno", _user.Userno)
            };

            ApplicationUserModel result = _db.GetApplicationUserDetails.FromSqlRaw("EXEC sp_getapplicationuserdetail @userno", param).AsNoTracking().AsEnumerable().FirstOrDefault();

            if (result == null)
            {
                return new ApiResponse()
                {
                    StatusCode = HttpStatusCode.Conflict,
                    IsSuccess = false,
                    ErrorMessages = "Invalid username and password."
                };
            }

            // Combine the base directory with the filename
            var _localurl = Path.Combine(ApplicationprofileUploadFolder, _user.Userno.ToString().Trim() + "." + _user.Profilepictype.Trim());
            var _publicurl = Path.Combine(ApplicationprofileUploadURL, _user.Userno.ToString().Trim() + "." + _user.Profilepictype.Trim());

            _user.Profileurl = Utils.CheckFileURL2(_publicurl, _localurl);

            var login = new LoginResponseModel
            {
                Expiration = Convert.ToDateTime(expiresIn),
                Data = _user,
                AccessToken = accessToken
            };

            return new ApiResponse
            {

                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                ErrorMessages = "",
                Data = result,
            };
        }

        public async Task<ApiResponse> RegisterAsync(RegistrationRequestDTO parameters)
        {
            var existingUser = await _db.Tblapplicationusers
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Emailaddress == parameters.Emailadd || u.Contactno == parameters.Mobileno);

            if (existingUser != null)
            {
                if (existingUser.Emailaddress == parameters.Emailadd)
                {
                    return new ApiResponse
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        IsSuccess = false,
                        ErrorMessages = "The provided email address is already registered.",
                        Data = 0
                    };
                }

                if (existingUser.Contactno == parameters.Mobileno)
                {
                    return new ApiResponse
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        IsSuccess = false,
                        ErrorMessages = "The provided mobile number is already registered.",
                        Data = 0
                    };
                }

            }

            var existingUserDetails = await _db.Tblapplicationusers
               .AsNoTracking()
               .FirstOrDefaultAsync(u => u.Lastname.Trim().ToUpper() == parameters.Lastname.Trim().ToUpper() && u.Firstname.Trim().ToUpper() == parameters.Firstname.Trim().ToUpper() && u.Birthdate.Date == parameters.Birthdate.Date);

            if (existingUser != null)
            {
                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.Conflict,
                    IsSuccess = false,
                    ErrorMessages = "The provided Last Name, First Name and Birthdate is already registered.",
                    Data = 0
                };
            }

            var strategy = _db.Database.CreateExecutionStrategy(); // Create retrying execution strategy
            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _db.Database.BeginTransactionAsync();
                try
                {
                    string hashedPassword = PasswordHelper.GenerateHashPassword(parameters.Password);

                    Tblapplicationuser user = new Tblapplicationuser
                    {
                        Lastname = parameters.Lastname,
                        Firstname = parameters.Firstname,
                        Miname = parameters.Miname,
                        Suffix = parameters.Suffix,
                        Contactno = parameters.Mobileno,
                        Emailaddress = parameters.Emailadd,
                        Birthdate = parameters.Birthdate,
                        Userpass = hashedPassword,
                        Dateencoded = DateTime.Now
                    };

                    await _db.Tblapplicationusers.AddAsync(user);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return new ApiResponse
                    {
                        StatusCode = HttpStatusCode.OK,
                        IsSuccess = true,
                        ErrorMessages = "",
                        Data = user.Userno
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

    }
}
