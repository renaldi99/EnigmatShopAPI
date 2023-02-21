using AutoMapper;
using EnigmatShopAPI.Dto;
using EnigmatShopAPI.Exceptions;
using EnigmatShopAPI.Helpers;
using EnigmatShopAPI.Models;
using EnigmatShopAPI.Services;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromQuery] string username, string password)
        {
            if (username == "" && password == "")
            {
                return BadRequest(new { code = 400, msg = "Invalid request" });
            }

            var checkUser = await _service.GetUserByUsername(username);
            var checkPass = Utility.DecryptPassword(checkUser.Password);

            if (checkUser == null || !checkPass.Equals(password))
            {
                return BadRequest(new { code = 400, msg = "User account isn't registered" });
            }

            // security
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
            var tokenResult = new JwtSecurityTokenHandler().WriteToken(tokenHandler);

            return Ok(new { code = StatusCodes.Status200OK, token = tokenResult, refreshToken = checkUser.RefreshToken });
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = 400, msg = "Invalid request" });
            }

            var token = model.Token;
            var refreshToken = model.RefreshToken;

            _ = GetPrincipalFromExpiredToken(model.Token, out ClaimsPrincipal principal);

            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            // get username from principal token
            string username = principal.Identity.Name;

            var user = await _service.GetUserByUsername(username);
            if (user == null || user.RefreshToken != model.RefreshToken)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            // security
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            // credential
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var _token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(principal.Claims),
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = credential
            };
            var tokenHandler = new JwtSecurityTokenHandler().CreateToken(_token);

            var newToken = new JwtSecurityTokenHandler().WriteToken(tokenHandler);
            var newRefreshToken = Utility.GenerateRefreshToken();

            // update refreshtoken in database when request new token !IMPORTANT FOR SECURITY
            user.RefreshToken = newRefreshToken;
            await _service.UpdateUser(user);

            return Ok(new { code = StatusCodes.Status200OK, token = newToken, refreshToken = newRefreshToken  });

        }

        // GET PRINCIPAL FOR REFRESH TOKEN
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token, out ClaimsPrincipal principal)
        {
            // untuk decode token
            // samakan config dengan di program.cs
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            // check validate token
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new TokenNotValidException("Invalid Token");
            }

            return principal;

        }

        // END POINT DISPLAY DATA BY MODEL DTO
        [AllowAnonymous]
        [HttpGet("GetUserByIdModelDTO")]
        public async Task<IActionResult> GetUserByModelDto([FromQuery] string username)
        {
            if (username == "")
            {
                return BadRequest(new { code = 400, msg = "Invalid request" });
            }

            User getUser = await _service.GetUserByUsername(username);

            if (getUser == null)
            {
                return BadRequest(new { code = 404, msg = "User doesn't exist" });
            }

            var dataMapperToDto = _mapper.Map<User, UserDto>(getUser);

            return Ok(new {code = StatusCodes.Status200OK, data = dataMapperToDto});
        } 
    }
}
