using System.Collections.Generic;
using HotChocolate;
using Microsoft.Extensions.Logging;
using PansyDev.Common.Application.Exceptions;
using Volo.Abp.Validation;

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
            switch (error.Exception)
            {
                case InvalidRequestException:
                {
                    return new Error(error.Exception.Message)
                        .WithCode("IR:001");
                }
                case AbpValidationException validationException:
                {
                    return new Error("Validation failed. See validationErrors for details.")
                        .WithExtensions(new Dictionary<string, object?>
                            {["validationErrors"] = validationException.ValidationErrors});
                }
                case not null:
                {
                    _logger.LogError(error.Exception, "An exception occurred on request handling");
                    return error;
                }
                default: return error;
            }
        }
    }
}
