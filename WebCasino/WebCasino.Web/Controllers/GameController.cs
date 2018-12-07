using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCasino.Service.Abstract;
using WebCasino.Web.Models.GameViewModels;

namespace WebCasino.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public IActionResult Index()
        {
            var board = this.gameService.GenerateBoard(4, 3);
            var model = this.gameService.GameResults(board);
            var dto = new GameViewModel()
            {
                Board = board,
                WinCoef = model.WinCoefficient
            };

            return View(dto);
        }
    }
}