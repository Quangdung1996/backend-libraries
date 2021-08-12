using HotChocolate;
using Microsoft.Extensions.Logging;
using PansyDev.Common.Application.Exceptions;

namespace PansyDev.Common.Web.GraphQL.Services
{
    internal class ErrorFilter : IErrorFilter
    {
        private readonly ILogger<ErrorFilter> _logger;

        public ErrorFilter(ILogger<ErrorFilter> logger)
        {
            _logger = logger;
        }

        public IError OnError(IError error)
        {
            if (error.Exception is InvalidRequestException)
            {
                return new Error(error.Exception.Message)
                    .WithCode("IR:001");
            }

            _logger.LogError(error.Exception, "An exception occurred on request handling");
            return error;
        }
    }
}
