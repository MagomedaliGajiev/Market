using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using WebApi.AuthorizationModel;
using WebApi.Db;
using WebApi.Repo;

namespace JWTAuth.Controllers
{
    public static class RsaTools
    {
        public static RSA GetPrivateKey()
        {
            var f = File.ReadAllText("rsa/private_key.pem");
            var rsa = RSA.Create();
            rsa.ImportFromPem(f);

            return rsa;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;

        public LoginController(IConfiguration configuration, IUserAuthenticationService authenticationService, IUserRepository userRepository)
        {
            _configuration = configuration;
            _authenticationService = authenticationService;
            _userRepository = userRepository;
        }

        private static UserRole RoleIdToUserRole(RoleId id)
        {
            if (id == RoleId.Admin)
            {
                return UserRole.Administrator;
            }
            else
            {
                return UserRole.User;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("AddAdmin")]
        public ActionResult AddAdmin([FromBody] LoginModel userLogin)
        {
            try
            {
                _userRepository.UserAdd(userLogin.UserName, userLogin.Password, WebApi.Db.RoleId.Admin);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);

            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("AddUser")]
        public ActionResult AddUser([FromBody] LoginModel userLogin)
        {
            try
            {
                _userRepository.UserAdd(userLogin.UserName, userLogin.Password, WebApi.Db.RoleId.User);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);

            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] LoginModel userLogin)
        {
            try
            {
                var roleId = _userRepository.UserCheck(userLogin.UserName, userLogin.Password);
                var user = new UserModel { UserName = userLogin.UserName, Role = RoleIdToUserRole(roleId) };
                var token = GenerateToken(user);

                return Ok(token);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        private string GenerateToken(UserModel user)
        {
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var key = new RsaSecurityKey(RsaTools.GetPrivateKey());
            var credentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);

            var claim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claim,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}