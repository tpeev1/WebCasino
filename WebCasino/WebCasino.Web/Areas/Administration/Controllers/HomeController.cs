using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCasino.Web.Areas.Administration.Controllers
{
	[Area("Administration")]
	[Authorize(Roles = "Administrator")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return this.View();
		}
	}
}