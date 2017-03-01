namespace Liquidacoes.Requests.Binder
{
    using Nancy;
    using System;
    using System.Linq;
    using Nancy.ModelBinding;
    using Liquidacoes.Requests.Model;

    public class FileUploadRequestBinder : IModelBinder
    {
        public object Bind(NancyContext context, Type modelType, object instance, BindingConfig configuration, params string[] blackList)
        {
            var fileUploadRequest = (instance as FileUploadRequest) ?? new FileUploadRequest();

            fileUploadRequest.UserId = GetUserId(context);
            fileUploadRequest.File = GetFile(context);

            return fileUploadRequest;
        }

        private int GetUserId(NancyContext context)
            => int.TryParse(context.Request.Form["userid"], out int userid) ? userid : 0;

        private HttpFile GetFile(NancyContext context) => context.Request.Files.FirstOrDefault();

        public bool CanBind(Type modelType) => modelType == typeof(FileUploadRequest);

    }
}