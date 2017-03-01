namespace Liquidacoes.Domain
{
    using System;

    public class Receipt
    {
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Boleto { get; set; }
        public string Nf { get; set; }
        public DateTime Emissao { get; set; }
        public decimal Bruto { get; set; }
        public decimal Liquido { get; set; }
        public DateTime Recebimento { get; set; }
        public decimal Principal { get; set; }
        public decimal Juros { get; set; }
    }
}