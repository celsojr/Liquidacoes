namespace Liquidacoes.Handlers.Model
{
    using Liquidacoes.Modules.EventFeed;
    using Liquidacoes.Modules.Liquidacao;

    public class HandlerUploadModel
    {
        public int UserId { get; set; }
        public ILiquidacaoStore LiquidacaoStore { get; set; }
        public IEventStore EventStore { get; set; }
        public string VirtualPath { get; set; }
    }
}