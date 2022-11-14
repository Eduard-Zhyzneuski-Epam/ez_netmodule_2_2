using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetModule2_2.BAL;

namespace NetModule2_2.NAL
{
    public class ErrorHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is EntityNotFoundException)
                context.Result = new NotFoundResult();
            if (context.Exception is InvalidEntityException)
                context.Result = new BadRequestObjectResult(context.Exception.Message);
        }
    }
}
