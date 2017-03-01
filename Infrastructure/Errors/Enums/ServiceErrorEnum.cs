namespace Liquidacoes.Infrastructure.Errors.Enums
{
    using System.Xml.Serialization;
    
    public enum ServiceErrorEnum
    {
        [XmlEnum("0")]
        GeneralError = 0,

        [XmlEnum("1")]
        ValidationError = 1,

        [XmlEnum("10")]
        NotFound = 10,

        [XmlEnum("20")]
        InternalServerError = 20,

        [XmlEnum("30")]
        Unauthorized = 30,

        [XmlEnum("31")]
        InvalidToken = 31
    }
}