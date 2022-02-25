using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FacultyAPR.API
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;
        public object Value { get; set; }
    }

    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;
        public void OnActionExecuting(ActionExecutingContext context) { }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                context.Result = new ObjectResult(exception.Value)
                {
                    StatusCode = exception.Status,
                };
                context.ExceptionHandled = true;
            }

            if (context.Exception is FacultyAPR.Storage.Sql.FormStoreInternalException e1)
            {
                context.Result = new ObjectResult(e1.Message)
                {
                    StatusCode = 500,
                };
                context.ExceptionHandled = true;
            }

            if (context.Exception is FacultyAPR.Storage.Sql.FormStoreValidationException e2)
            {
                context.Result = new ObjectResult(e2.Message)
                {
                    StatusCode = 400,
                };
                context.ExceptionHandled = true;
            }

            if (context.Exception is FacultyAPR.Storage.Sql.UserStoreValidationException e3)
            {
                context.Result = new ObjectResult(e3.Message)
                {
                    StatusCode = 400,
                };
                context.ExceptionHandled = true;
            }

            if (context.Exception is System.Exception e4)
            {
                context.Result = new ObjectResult(e4.Message)
                {
                    StatusCode = 500,
                };
                context.ExceptionHandled = true;
            }
        }
}
}