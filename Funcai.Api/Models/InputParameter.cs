using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Funcai.Api.Models
{
    public class InputParameter
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}