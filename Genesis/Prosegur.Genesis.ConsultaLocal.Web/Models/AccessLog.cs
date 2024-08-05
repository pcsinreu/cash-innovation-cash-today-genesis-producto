using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Concepto.Models
{
    public class AccessLog
    {
        public string login { get; set; }
        public string modulo { get; set; }
        public bool accesoOtorgado { get; set; }
    }
}