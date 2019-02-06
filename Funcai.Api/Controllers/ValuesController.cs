using Funcai.Api.Models;
using Funcai.Api.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace Funcai.Api.Controllers
{
    public class ValuesController : ApiController
    {
        // POST api/values
        public IEnumerable<string> Post([FromBody]Request request)
        {
            var valuesService = new ValuesService();
            var values = valuesService.GetValues();

            var compilationService = new CompilationService();
            return compilationService.ExecuteRequest(request, values);
        }
    }
}
