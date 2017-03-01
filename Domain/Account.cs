namespace Liquidacoes.Domain
{
    using System.Linq;
    using System.Reflection;

    internal class Account
    {
        private string data;
        private string contaDebito;
        private string fcont;
        private string contaCredito;
        private string centroCusto;
        private string historico;
        private string especie;

        public string Data
        {
            get { return data; }
            set { data = FillWithSpace(value, 4); }
        }
        
        public string ContaDebito
        {
            get { return contaDebito; }
            set { contaDebito = FillWithSpace(value, 16); }
        }
        
        public string ContaCredito 
        { 
            get { return contaCredito; }
            set { contaCredito = FillWithSpace(value, 16); } 
        }

        public string CentroCusto 
        { 
            get { return centroCusto; }
            set { centroCusto = FillWithSpace(value, 10); }
        }

        public string Historico 
        { 
            get { return historico; }
            set { historico = FillWithSpace(value, 255); }
        }

        public string Especie 
        { 
            get { return especie; }
            set { especie = FillWithSpace(value, 14); }
        }

        public string Fcont
        {
            get { return fcont; }
            set { fcont = FillWithSpace(value, 1); }
        }
                
        public override string ToString()
        {
            var type = GetType();
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var properties = type.GetProperties(flags);

            return string.Concat(properties.Select(pi => pi.GetValue(this, null)));
        }

        private string FillWithSpace(string value, int length)
        {
            if (value.Length > length)
                return value.Substring(0, length);

            while (value.Length < length)
                value += " ";

            return value;
        }
    }
}