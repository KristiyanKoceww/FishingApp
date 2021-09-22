using Microsoft.AspNetCore.Mvc;
using MyFishingApp.Data.Models;
using MyFishingApp.Services.Data.AppUsers;
using MyFishingApp.Services.Data.InputModels.AppUsersInputModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFishingApp.Web.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AppUsersController : ControllerBase
    {
        private readonly IAppUser userService;

        public AppUsersController(IAppUser userService)
        {
            this.userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(UserInputModel userInputModel)
        {
           await this.userService.CreateAsync(userInputModel);

            return Ok();
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
            await this.userService.UpdateUserAsync(userInputModel,userId);
            return Ok();
        }

        [HttpGet("getUser/id")]
        public string GetUserById(string userId)
        {
            var user =  this.userService.GetById(userId);

            var json = JsonConvert.SerializeObject(user);

            return json;
        }
    }
}
