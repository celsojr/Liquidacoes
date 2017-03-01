namespace Liquidacoes.Infrastructure.Validation.Line
{
    internal interface ILineValidation
    {
        LineValidation IsHeader();
        LineValidation IsEmpty();
        LineValidationResult IsOk();
    }
}