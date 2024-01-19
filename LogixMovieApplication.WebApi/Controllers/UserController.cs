using LogixMovieApplication.WebApi.Entities;
using LogixMovieApplication.WebApi.Interfaces;
using LogixMovieApplication.WebApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace LogixMovieApplication.WebApi.Controllers
{

    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration, ILogger<UserController> logger) : base(logger)
        {
            _userService = userService;
            _configuration = configuration;
            _logger = logger;
        }
        [Authorize]
        [HttpGet]
        [Route("/api/user/me")]
        public async Task<IActionResult> Me()
        {
            _logger.LogInformation("GET User/Me - {0}. {1}", LoggedInUserId, DateTime.UtcNow.ToLongTimeString());
            try
            {
                var userInfo = await _userService.GetById(LoggedInUserId);
                if (userInfo == null)
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(new UserVM()
                    {
                        Id = userInfo.Id,
                        Email = userInfo.Email
                    });
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("GET User/Me - {0}, message:{1}. {2}", LoggedInUserId, ex.Message, DateTime.UtcNow.ToLongTimeString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("/api/user/register")]
        public async Task<IActionResult> Register([FromBody] RegisterVM model)
        {
            try
            {
                _logger.LogInformation("POST User/Register - Email:{0}. {1}", model.Email, DateTime.UtcNow.ToLongTimeString());
                await _userService.Register(model);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("POST User/Register - Email:{0},message:{1}. {2}", model.Email, ex.Message, DateTime.UtcNow.ToLongTimeString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("/api/user/login")]
        public async Task<IActionResult> Login([FromBody] LoginVM model)
        {
            try
            {
                _logger.LogInformation("POST User/Login - Email:{0}. {1}", model.Email, DateTime.UtcNow.ToLongTimeString());
                var isValid = await _userService.Login(model.Email, model.Password);
                if (!isValid)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
                else
                {
                    var user = await _userService.GetByEmail(model.Email);
                    string accessToken = GenerateJSONWebToken(user);
                    if (!string.IsNullOrWhiteSpace(accessToken))
                    {
                        return Ok(new
                        {
                            token_type = "Bearer",
                            access_token = accessToken,
                            expires_in = int.Parse(_configuration["Jwt:AccessTokenExpiredMinutes"]) * 60,
                        });
                    }
                    return BadRequest(new { message = "Login fail" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("POST User/LoginVM - Email:{0},message:{1}. {2}", model.Email, ex.Message, DateTime.UtcNow.ToLongTimeString());
                return BadRequest(new { message = ex.Message });
            }
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            try
            {
                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier,userInfo.Id.ToString())
                };
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_configuration["Jwt:Audience"],
                   _configuration["Jwt:Issuer"],
                   claims,
                   expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:AccessTokenExpiredMinutes"])),
                   signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}
