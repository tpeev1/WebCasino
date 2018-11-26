using Microsoft.AspNetCore.Mvc;

namespace WebCasino.Web.Areas.Administration.Controllers
{
	[Area("Administration")]
	public class TransactionsController : Controller
	{
		public IActionResult History()
		{
			return View();
		}
	}
}