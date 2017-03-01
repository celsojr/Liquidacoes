namespace Liquidacoes.Modules.Liquidacao
{
    public interface ILiquidacaoStore
    {
        Liquidacao Get(int userId);
        void Save(Liquidacao liquidacao);
    }
}