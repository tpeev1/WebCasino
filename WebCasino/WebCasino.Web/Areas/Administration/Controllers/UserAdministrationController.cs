using Microsoft.AspNetCore.Mvc;
using System;
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

		public async Task<IActionResult> Index(UsersIndexViewModel model)
		{
			if (string.IsNullOrWhiteSpace(model.SearchText))
			{
				model.Users = await this.service.GetAllUsers(model.Page, 10);
				model.TotalPages = (int)Math.Ceiling(await this.service.Total() / (double)10);
			}
			else
			{
				model.Users = this.service.ListByContainingText(model.SearchText, model.Page, 10);
				model.TotalPages = (int)Math.Ceiling(this.service.TotalContainingText(model.SearchText) / (double)10);
			}

			return View(model);
		}

		public async Task<IActionResult> Details(string id)
		{
			var user = await this.service.RetrieveUser(id);

			var model = new UserViewModel(user);

			return View(model);
		}
	}
}