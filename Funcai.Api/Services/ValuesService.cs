using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Funcai.Api.Services
{
    public class ValuesService
    {
        public List<string> GetValues()
        {
            return new List<string>
            {
                "value1",
                "value2",
                "value3",
                "value4",
                "value5"
            };
        }
    }
}