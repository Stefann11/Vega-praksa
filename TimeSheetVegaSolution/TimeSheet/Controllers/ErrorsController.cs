using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.CustomException;

namespace TimeSheet.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public MyErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var code = exception is HttpStatusException httpException ? (int)httpException.Status : 500;
            Response.StatusCode = code; 
            return new MyErrorResponse(exception, code); 
        }
    }
}
