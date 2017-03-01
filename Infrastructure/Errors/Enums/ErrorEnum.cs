namespace Liquidacoes.Infrastructure.Errors.Enums
{
    using System.ComponentModel;

    public enum ErrorEnum
    {
        [Description("CNPJ inválido")]
        CnpjInvalido,
        [Description("Pagamento não encontrado")]
        PgtoNaoEncontrado,
        [Description("Arquivo nulo ou não enviado")]
        FileNull,
        [Description("Tamanho máximo de {0} bytes excedido")]
        FileSizeExceeded,
        [Description("Permitido apenas arquivos do tipo '.csv'")]
        FileExtensionNotAllowed
    }
}