namespace Liquidacoes.Modules.Liquidacao
{
    using System.Collections.Generic;

    public class LiquidacaoStore : ILiquidacaoStore
    {
        private static readonly Dictionary<int, Liquidacao> database = new Dictionary<int, Liquidacao>();

        public Liquidacao Get(int userId)
        {
            if (!database.ContainsKey(userId))
                database[userId] = new Liquidacao(userId);
            return database[userId];
        }

        public void Save(Liquidacao shoppingCart)
        {
            // Not implemented. Saving would be working with a real DB only.
        }
        
    }
}