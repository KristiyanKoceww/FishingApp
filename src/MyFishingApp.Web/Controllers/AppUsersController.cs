using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFishingApp.Services.Data.AppUsers;
using MyFishingApp.Services.Data.InputModels.AppUsersInputModels;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyFishingApp.Services.Data.NEWJWTSERVICE;
using System.Linq;
using MyFishingApp.Services.Data.JwtService;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using MyFishingApp.Data.Models;

namespace MyFishingApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppUsersController : ControllerBase
    {
        private readonly IAppUser userService;
        private readonly ILogger<AppUsersController> logger;
        private readonly SignInManager signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JWTAuthService jwtAuthService;

        public AppUsersController(
            IAppUser userService,
            ILogger<AppUsersController> logger,
            SignInManager signInManager,
            UserManager<ApplicationUser> userManager,
            JWTAuthService jwtAuthService)
        {
            this.userService = userService;
            this.logger = logger;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.jwtAuthService = jwtAuthService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserInputModel model)
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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var result = await this.signInManager.SignIn(request.UserName, request.Password);

            if (!result.Success) return Unauthorized();

            this.logger.LogInformation($"User [{request.UserName}] logged in the system.");

            return Ok(new LoginResult
            {
                UserId = result.User.Id,
                UserName = result.User.UserName,
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken
            });
        }

        [HttpPost("user")]
        public string UserAuth([FromBody] string accessToken)
        {

            ClaimsPrincipal claimsPrincipal = this.jwtAuthService.GetPrincipalFromToken(accessToken);
            string id = claimsPrincipal.Claims.First(c => c.Type == "id").Value;

            var userId = JsonConvert.SerializeObject(id);
            return userId;

        }

        [HttpPost("refreshtoken")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var result = await this.signInManager.RefreshToken(request.AccessToken, request.RefreshToken);

            if (!result.Success) return Unauthorized();

            return Ok(new LoginResult
            {
                UserName = result.User.Email,
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken
            });
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
        public async Task<IActionResult> UpdateUser([FromForm] UserInputModel userInputModel, string userId)
        {
            await this.userService.UpdateUserAsync(userInputModel, userId);
            return Ok();
        }

        [Authorize]
        [HttpPost("ChangeProfilePicture")]
        public async Task<IActionResult> ChangeProfilePicture([FromForm] ChangePictureInputModel changePictureInputModel)
        {
            try
            {
                await userService.ChangeUserProfilePicture(changePictureInputModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("getUser/id")]
        public string GetUserById(string userId)
        {
            var user = this.userService.GetById(userId);

            var json = JsonConvert.SerializeObject(user);

            return json;
        }
    }
}
