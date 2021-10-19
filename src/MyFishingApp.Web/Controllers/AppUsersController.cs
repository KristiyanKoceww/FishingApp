using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyFishingApp.Data.Models;
using MyFishingApp.Services.Data.AppUsers;
using MyFishingApp.Services.Data.InputModels.AppUsersInputModels;
using MyFishingApp.Services.Data.InputModels.AppUsersLoginAndRegModels;
using MyFishingApp.Services.Data.Jwt;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyFishingApp.Web.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AppUsersController : ControllerBase
    {
        private readonly IAppUser userService;
        private readonly AppSettings _appSettings;
        private readonly IJwtService jwtService;



        public AppUsersController(
            IAppUser userService,
            IOptions<AppSettings> appSettings,
            IJwtService jwtService)
        {
            this.userService = userService;
            _appSettings = appSettings.Value;
            this.jwtService = jwtService;
        }

        //[AllowAnonymous]
        //[HttpPost("authenticate")]
        //public IActionResult Authenticate( AuthenticateModel model)
        //{
        //    var user = userService.Authenticate(model.Username, model.Password);

        //    if (user == null)
        //        return BadRequest(new { message = "Username or password is incorrect" });

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, user.Id.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(1),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token =  tokenHandler.CreateToken(tokenDescriptor);
        //    var tokenString = tokenHandler.WriteToken(token);

        //    // return basic user info and authentication token
        //    return Ok(new
        //    {
        //        Id = user.Id,
        //        Username = user.UserName,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        MiddleName = user.MiddleName,
        //        Age = user.Age,
        //        Gender = user.Gender.ToString(),
        //        MainImageUrl = user.MainImageUrl,
        //        Phone = user.Phone,
        //        Token = tokenString,
        //    });
        //}
        //[HttpPost("logOut")]
        //public IActionResult LogOut()
        //{
        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    return null;

        //}
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserInputModel model)
        {

            try
            {
                await userService.CreateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginInputModel loginInputModel)
        {
            var user = this.userService.GetByUsername(loginInputModel.Username);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var userAuth = this.userService.Authenticate(loginInputModel.Username, loginInputModel.Password);
            if (userAuth == null)
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var jwt = jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true , SameSite = SameSiteMode.None, Secure = true });
            // SameSite = SameSiteMode.None , Secure = true
            return Ok(new
            {
                message = "Success",
            }) ;
        }


        [HttpGet("user")]
        public IActionResult UserAuth()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = jwtService.Verify(jwt);

                var userId = token.Issuer;

                var user = this.userService.GetById(userId);

                return Ok(user);
            }
            catch (Exception e)
            {
                return Unauthorized();
            }

        }

        [HttpPost("logout")]
        public IActionResult LogOut()
        {

            Response.Cookies.Delete("jwt");
            return Ok(new { mesage = "Success" });

        }


        [Authorize]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await this.userService.DeleteAsync(userId);

            return Ok();
        }

        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser(UserInputModel userInputModel, string userId)
        {
            await this.userService.UpdateUserAsync(userInputModel, userId);
            return Ok();
        }

        [Authorize]
        [HttpGet("getUser/id")]
        public string GetUserById(string userId)
        {
            var user = this.userService.GetById(userId);

            var json = JsonConvert.SerializeObject(user);

            return json;
        }
    }
}
