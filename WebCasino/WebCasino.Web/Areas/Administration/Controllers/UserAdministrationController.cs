using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebCasino.Service.Abstract;
using WebCasino.Web.Areas.Administration.Models;

namespace WebCasino.Web.Areas.Administration.Controllers
{
	[Area("Administration")]
	public class UserAdministrationController : Controller
	{
		private readonly IUserService service;

		public UserAdministrationController(IUserService service)
		{
			this.service = service ?? throw new System.ArgumentNullException(nameof(service));
		}

		public async Task<IActionResult> Index()
		{
			var usersQuery = await this.service.GetAllUsers();

			var viewModel = new UsersIndexViewModel(usersQuery);

			return View(viewModel);
		}
	}
}