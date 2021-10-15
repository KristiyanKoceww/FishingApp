using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyFishingApp.Data.Models;
using MyFishingApp.Services.Data.AppUsers;
using MyFishingApp.Services.Data.InputModels.AppUsersInputModels;
using MyFishingApp.Services.Data.InputModels.AppUsersLoginAndRegModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyFishingApp.Web.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AppUsersController : ControllerBase
    {
        private readonly IAppUser userService;
        private readonly AppSettings _appSettings;
        

        public AppUsersController(
            IAppUser userService,
            IOptions<AppSettings> appSettings)
        {
            this.userService = userService;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate( AuthenticateModel model)
        {
            var user = userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token =  tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Age = user.Age,
                Gender = user.Gender.ToString(),
                MainImageUrl = user.MainImageUrl,
                Phone = user.Phone,
                Token = tokenString,
            });
        }
        [HttpPost("logOut")]
        public IActionResult LogOut()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return null;
            
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register (UserInputModel model)
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
            await this.userService.UpdateUserAsync(userInputModel,userId);
            return Ok();
        }

        [Authorize]
        [HttpGet("getUser/id")]
        public string GetUserById(string userId)
        {
            var user =  this.userService.GetById(userId);

            var json = JsonConvert.SerializeObject(user);

            return json;
        }
    }
}
