namespace MyFishingApp.Web.Areas.Administration.Controllers
{
    using MyFishingApp.Common;
    using MyFishingApp.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
