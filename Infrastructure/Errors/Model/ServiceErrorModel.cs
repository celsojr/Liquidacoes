namespace Liquidacoes.Infrastructure.Errors.Model
{
    using Liquidacoes.Infrastructure.Errors.Enums;

    public class ServiceErrorModel
    {
        public ServiceErrorEnum Code { get; set; }

        public string Details { get; set; }

        public ServiceErrorModel()
        {
        }

        public ServiceErrorModel(ServiceErrorEnum code, string details)
        {
            Code = code;
            Details = details;
        }
    }
}
