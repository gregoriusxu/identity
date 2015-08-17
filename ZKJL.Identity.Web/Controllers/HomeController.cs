using System.Web.Mvc;
using Abp.Dependency;
using Abp.Web.Mvc.Authorization;
using ZKJL.Identity.Application.Authorization;
using ZKJL.Identity.Application.Menues;

namespace ZKJL.Identity.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : IdentityControllerBase
    {
        private readonly IMenuAppService _menuAppService;

        public HomeController(IMenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }

        public ActionResult Index()
        {
            IocManager.Instance.Resolve<IdentityAuthorizationProvider>().SetPermissions();//设置权限

            _menuAppService.SetMenu();//设置菜单

            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
    }
}