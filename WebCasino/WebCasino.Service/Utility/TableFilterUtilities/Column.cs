using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCasino.Web.Utilities.TableFilterUtilities
{
    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }
}
