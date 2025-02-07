using Fiap.Hackathon.Common.Shared.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Fiap.Hackathon.Common.Shared.Exceptions;
using Fiap.Hackathon.Common.Shared.Responses;
using Fiap.Hackathon.Medicos.Domain.Exceptions;

namespace Fiap.Hackathon.Medicos.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var status = 500;
            if (context.Exception is DomainException
                    || context.Exception.InnerException is DomainException
                    )
            {
                status = 400;
            }
            else if (context.Exception is NotFoundException
                    || context.Exception.InnerException is NotFoundException
                    )
            {
                status = 404;
            }
            else if ((context.Exception is CPFRegisteredException
                    || context.Exception.InnerException is CPFRegisteredException))
            {
                status = 409;
            }

           var mensagem = status == 400 || status == 404 || status == 409  ? (
                            context.Exception.InnerException == null ? context.Exception.Message
                            : context.Exception.InnerException.Message)
                                    : "Ocorreu uma falha inesperada no servidor";
            var response = context.HttpContext.Response;


            response.StatusCode = status;
            response.ContentType = "application/json";
            context.Result = new JsonResult(new DefaultResponse(false, mensagem));
        }
    }
}
