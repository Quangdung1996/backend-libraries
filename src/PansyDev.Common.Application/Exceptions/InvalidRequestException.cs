using System;

namespace PansyDev.Common.Application.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(string? message) : base(message) { }
    }
}