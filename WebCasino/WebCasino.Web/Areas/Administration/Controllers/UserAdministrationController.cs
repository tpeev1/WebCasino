using Microsoft.AspNetCore.Mvc;

namespace WebCasino.Web.Areas.Administration.Controllers
{
	[Area("Administration")]
	public class UserAdministrationController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}