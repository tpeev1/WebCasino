using System.Collections.Generic;
using System.Linq;
using WebCasino.Entities;

namespace WebCasino.Web.Areas.Administration.Models
{
	public class UsersIndexViewModel
	{
		public  IEnumerable<User> Users { get; set; }

        public int TotalPages { get; set; }

        public int Page { get; set; } = 1;

        public int PreviousPage => this.Page ==
            1 ? 1 : this.Page - 1;

        public int NextPage => this.Page ==
            this.TotalPages ? this.TotalPages : this.Page + 1;

        public string SearchText { get; set; } = string.Empty;

     
	}
}