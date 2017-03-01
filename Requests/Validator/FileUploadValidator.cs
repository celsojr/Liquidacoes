namespace Liquidacoes.Requests.Validator
{
    using Nancy;
    using System.IO;
    using FluentValidation;
    using FluentValidation.Results;
    using Liquidacoes.Requests.Model;
    using Liquidacoes.Infrastructure.Settings;
    using Liquidacoes.Infrastructure.Errors.Enums;
    using static Liquidacoes.Infrastructure.Errors.Services.ErrorServices;

    public class FileUploadValidator : AbstractValidator<FileUploadRequest>
    {
        public FileUploadValidator(IApplicationSettings appSettings)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            var maxUploadSize = GetMaxUploadSizeAllowed(appSettings?.FileSettings?.MaxFileSize ?? 4);

            RuleFor(x => x.UserId)
                .InclusiveBetween(1, int.MaxValue);

            RuleFor(x => x.File)
                .Must((request, file) => IsValid(request.File))
                .WithMessage(GetErrorMessage(ErrorEnum.FileExtensionNotAllowed))
                .Must((request, file) => request.File.Value.Length < maxUploadSize)
                .WithMessage(GetFileSizeErrorMessage(ErrorEnum.FileSizeExceeded, maxUploadSize));
        }

        public override ValidationResult Validate(FileUploadRequest instance)
        {
            return instance?.File == null 
                ? new ValidationResult(new [] 
                    { 
                        new ValidationFailure("File", GetErrorMessage(ErrorEnum.FileNull))
                    }) 
                : base.Validate(instance);
        }

        private bool IsValid(HttpFile file) => !CheckExtension(file.Name);
        
        private bool CheckExtension(string fileName) => Path.GetExtension(fileName).ToLower() != ".csv";

        private string GetFileSizeErrorMessage(ErrorEnum errorType, long maxFileSize)
            => string.Format(GetErrorMessage(errorType), maxFileSize);

        /// <summary>
        /// Returns the maximum allowed file size as defined in the 'appsettings.json'
        /// where each unit represents 1 megabyte. Ex. 4 units = 4 megabytes = 4194304 bytes.
        /// </summary>
        private long GetMaxUploadSizeAllowed(int maxFileSize) => maxFileSize * 1024 * 1024;

    }
}