using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Service.DTO.Game;

namespace WebCasino.Web.Models.GameViewModels
{
    public class GameViewModel
    {
        public GameSymbolDTO[,] Board { get; set; }

        public double WinCoef { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid bet.")]
        public double BetAmount { get; set; }
    }
}
