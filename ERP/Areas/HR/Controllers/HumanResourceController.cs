using ERP.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ERP.Areas.HR.Controllers
{
    [Area("HR"), Authorize]
    public class HumanResourceController : SettingsController
    {
        private readonly IConfiguration _configuration;
        public HumanResourceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
