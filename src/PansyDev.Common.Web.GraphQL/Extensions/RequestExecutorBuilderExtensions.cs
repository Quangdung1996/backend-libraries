using HotChocolate;
using HotChocolate.Execution.Configuration;
using HotChocolate.Language;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using PansyDev.Common.Application.Models;

namespace PansyDev.Common.Web.GraphQL.Extensions
{
    public static class RequestExecutorBuilderExtensions
    {
        private const string OperationFailureResultType = "OperationFailureResult";

        public static void AddCommonTypes(this IRequestExecutorBuilder builder)
        {
            builder.AddObjectType<OperationFailureResult>(x => x.Name(OperationFailureResultType));
            builder.AddTypeConverter<OperationFailureConverter>();
        }

        public static void AddOperationResult<T>(this IRequestExecutorBuilder builder, string resultName)
        {
            var successResultType = $"{resultName}OperationSuccessResult";

            builder.ConfigureSchema(schemaBuilder => schemaBuilder.AddUnionType<OperationResult<T>>(x => x
                .Name($"{resultName}OperationResult")
                .Type(new NamedTypeNode(OperationFailureResultType))
                .Type(new NamedTypeNode(successResultType))
                .ResolveAbstractType((t, result) =>
                {
                    var nonNullType = (NonNullType)t.Selection.Type;
                    var unionType = (UnionType)nonNullType.Type;
                    return result is IOperationFailureResult
                        ? unionType.Types[OperationFailureResultType]
                        : unionType.Types[successResultType];
                })));

            builder.AddObjectType<OperationSuccessResult<T>>(x => x.Name(successResultType));
        }
    }
}
