using System;
using HotChocolate.Utilities;
using PansyDev.Common.Application.Models;

namespace PansyDev.Common.Web.GraphQL.Extensions
{
    internal class OperationFailureConverter : IChangeTypeProvider
    {
        public bool TryCreateConverter(Type source, Type target, ChangeTypeProvider root, out ChangeType converter)
        {
            converter = null!;

            if (!source.IsGenericType || source.GetGenericTypeDefinition() != typeof(OperationFailureResult<>))
                return false;

            if (target != typeof(OperationFailureResult))
                return false;

            converter = instance => new OperationFailureResult(((IOperationFailureResult)instance!).Code);
            return true;
        }
    }
}
