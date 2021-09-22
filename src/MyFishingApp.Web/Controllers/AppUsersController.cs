using Microsoft.AspNetCore.Mvc;
using MyFishingApp.Services.Data.AppUsers;
using MyFishingApp.Services.Data.InputModels.AppUsersInputModels;
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
        public void CreateUser(UserInputModel userInputModel)
        {
            this.userService.CreateAsync(userInputModel);
        }

        [HttpPost("delete")]
        public void DeleteUser(string userId)
        {
            this.userService.DeleteAsync(userId);
        }

        [HttpPost("update")]
        public void UpdateUser(UserInputModel userInputModel, string userId)
        {
            this.userService.UpdateUserAsync(userInputModel,userId);
        }
    }
}
