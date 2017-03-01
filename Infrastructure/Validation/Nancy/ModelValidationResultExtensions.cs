namespace Liquidacoes.Infrastructure.Validation.Nancy
{
    using System.Linq;
    using global::Nancy.Validation;

    public static class ModelValidationResultExtensions
    {
        public static string GetDetailedErrorMessage(this ModelValidationResult modelValidationResult)
        {
            var formattedErrors = modelValidationResult.Errors
                .Select(x => new { Key = x.Key, Errors = x.Value.Select(y => y.ErrorMessage) })
                .Select(x => $"Parameter = {x.Key}, Errors = ({string.Join(", ", x.Errors)})");

            return $"Validation failed for Request Parameters: ({string.Join(", ", formattedErrors)})";
        }
    }
}
