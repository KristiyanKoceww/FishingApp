using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyFishingApp.Services.Data.AppUsers;
using MyFishingApp.Services.Data.InputModels.AppUsersInputModels;
using MyFishingApp.Services.Data.Jwt;
using Newtonsoft.Json;
using System;
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

            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true });
            // SameSite = SameSiteMode.None , Secure = true
            return Ok(new
            {
                message = "Success",
            });
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
                
                return Ok(new { userId, user });
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


        
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await this.userService.DeleteAsync(userId);

            return Ok();
        }

        
        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser(UserInputModel userInputModel, string userId)
        {
            await this.userService.UpdateUserAsync(userInputModel, userId);
            return Ok();
        }

        
        [HttpGet("getUser/id")]
        public string GetUserById(string userId)
        {
            var user = this.userService.GetById(userId);

            var json = JsonConvert.SerializeObject(user);

            return json;
        }
    }
}
