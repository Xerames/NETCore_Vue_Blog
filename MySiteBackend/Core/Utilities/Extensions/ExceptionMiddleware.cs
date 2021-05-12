using Core.Exceptions;
using Core.Utilities.Responses.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }
        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal Server Error";
            if (e.GetType() == typeof(ValidationException))
            {
                IEnumerable<ValidationFailure> errors;
                errors = ((ValidationException)e).Errors;
                httpContext.Response.StatusCode = 400;
                var validationerror = JsonConvert.SerializeObject(new ErrorResponse(400, errors.Select(x => x.ErrorMessage).ToList()));
                return httpContext.Response.WriteAsync(validationerror);
            }
            
            else if (e.InnerException is ApiException || e.GetType() == typeof(ApiException))
            {
                var ex = e.InnerException != null ? (ApiException)e.InnerException : (ApiException)e;
                httpContext.Response.StatusCode = ex.StatusCode;
                var apierror = JsonConvert.SerializeObject(new ErrorResponse(ex.StatusCode, ex.Errors));
                return httpContext.Response.WriteAsync(apierror);
            }
           
            var error = JsonConvert.SerializeObject(new ErrorResponse(httpContext.Response.StatusCode, message));
            return httpContext.Response.WriteAsync(error);
        }
    }
}
