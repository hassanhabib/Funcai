using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Funcai.Api.Models
{
    public class Request
    {
        public List<string> Usings { get; set; }
        public List<InputParameter> Parameters { get; set; }
        public string Code { get; set; }
    }
}