namespace Liquidacoes.Infrastructure.Validation.Line
{
    using System;
    using System.Linq;
    using Liquidacoes.Domain;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    internal class LineValidation : ILineValidation
    {
        private readonly IList<string> fields;

        private bool isValid { get; set; } = true;

        public LineValidation(IList<string> fields)
        {
            this.fields = fields;
        }

        public static LineValidation ValidateLine(string line)
        {
            var columns = line.Split(';');
            
            if (columns.Length != 12)
            {
                throw new InvalidOperationException("Número de colunas incorreto");
            }

            return new LineValidation(columns);
        }

        public LineValidation IsHeader()
        {
            var firstColumn = fields.ToArray()[0];

            if (firstColumn.StartsWith("PROGRAMA"))
            {
                isValid = false;
                return this;
            }

            if (firstColumn.StartsWith("Período"))
            {
                isValid = false;
                return this;
            }

            if (firstColumn.StartsWith("A Receber"))
            {
                isValid = false;
                return this;
            }
            
            if (firstColumn.StartsWith("Razão Social"))
            {
                isValid = false;
                return this;
            }

            return this;
        }

        public LineValidation IsEmpty()
        {
            if (isValid)
            {
                var firstColumn = fields.ToArray()[0];
                isValid = !string.IsNullOrWhiteSpace(firstColumn);
            }

            return this;
        }

        public LineValidationResult IsOk() => new LineValidationResult(isValid, fields);

        public static bool ValidateCnpj(string cnpj) => (cnpj.Length < 14 || cnpj == $"{0:D14}");
    }

    internal class LineValidationResult
    {
        public bool IsValid { get; private set; }

        public Receipt Receipt { get; private set; }

        public LineValidationResult(bool isValid, IList<string> fields)
        {
            if (isValid)
            {
                IsValid = isValid;

                Receipt = new Receipt
                {
                    RazaoSocial = fields.ToArray()[0],
                    Cnpj = Regex.Replace(fields.ToArray()[1], "[^0-9]", ""),
                    Boleto = fields.ToArray()[2],
                    Nf = fields.ToArray()[3],
                    Emissao = DateTime.Parse(fields.ToArray()[4]),
                    Bruto = decimal.Parse(fields.ToArray()[5]),
                    Liquido = decimal.Parse(fields.ToArray()[6]),
                    Recebimento = DateTime.Parse(fields.ToArray()[7]),
                    Principal = decimal.Parse(fields.ToArray()[8]),
                    Juros = decimal.Parse(fields.ToArray()[9])
                };
            }
        }
        

    }
}