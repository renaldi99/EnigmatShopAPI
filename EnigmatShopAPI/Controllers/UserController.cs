using AutoMapper;
using EnigmatShopAPI.Dto;
using EnigmatShopAPI.Helpers;
using EnigmatShopAPI.Models;
using EnigmatShopAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utility = EnigmatShopAPI.Helpers.Utility;

namespace EnigmatShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(IUserService service, IMapper mapper, IConfiguration configuration)
        {
            _service = service;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = 400, msg = "Invalid request" });
            }

            // create refresh token
            user.Role = "USER";
            user.RefreshToken = Utility.GenerateRefreshToken();
            user.Password = Utility.EncryptPassword(user.Password);

            // Mapper disini
            var userMapper = _mapper.Map<User>(user);

            var checkUserExist = await _service.GetUserByUsername(userMapper.Username);
            if (checkUserExist != null)
            {
                return BadRequest(new { code = 400, msg = "Username already exist" });
            }
           
            var createUser = await _service.CreateUser(userMapper);
            if (createUser == 0)
            {
                return BadRequest(new { code = 400, msg = "Invalid request" });
            }

            return Ok(new { code = 200, msg = "Success create user" });
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromQuery] string username, string password)
        {
            if (username == "" && password == "")
            {
                return BadRequest(new { code = 400, msg = "Invalid request" });
            }

            var checkUser = await _service.GetUserByUsername(username);
            var passCheck = Utility.DecryptPassword(checkUser.Password);

            if (checkUser == null || !passCheck.Equals(password))
            {
                return BadRequest(new { code = 400, msg = "User account isn't registered" });
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            // credential
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, checkUser.Username)
            };

            var _token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = credential
            };
            var tokenHandler = new JwtSecurityTokenHandler().CreateToken(_token);

            return Ok(new { code = StatusCodes.Status200OK, token = new JwtSecurityTokenHandler().WriteToken(tokenHandler) , refreshToken = checkUser.RefreshToken });

        }
    }
}
