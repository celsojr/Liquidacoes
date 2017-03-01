namespace Liquidacoes.Modules.Common
{
    using System.IO;
    using global::Nancy;
    using System.Threading.Tasks;
    using global::Nancy.Validation;
    using global::Nancy.ModelBinding;
    using Liquidacoes.Infrastructure.Specification.Exceptions;

    public static class ModuleExtensions
    {
        public static TModel CustomBindAndValidate<TModel>(this NancyModule module)
        {
            TModel model = module.Bind<TModel>();

            Validate(module, model);

            return model;
        }

        private static void Validate<TModel>(NancyModule module, TModel model)
        {
            if (model != null)
            {
                var modelValidationResult = module.Validate(model);
                if (!modelValidationResult.IsValid)
                {
                    throw new ValidationServiceErrorException(modelValidationResult);
                }
            }
        }

        public static async Task<Stream> GetStreamAsync(this NancyModule module, string value)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            await writer.WriteAsync(value).ConfigureAwait(false);
            await writer.FlushAsync().ConfigureAwait(false);

            stream.Position = 0;
            return stream;
        }

    }
}
