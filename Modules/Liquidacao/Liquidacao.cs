namespace Liquidacoes.Modules.Liquidacao
{
    using System.Linq;
    using System.Collections.Generic;
    using Liquidacoes.Modules.EventFeed;

    public class Liquidacao
    {
        public int UserId { get; }
        private HashSet<LiquidacaoItem> items = new HashSet<LiquidacaoItem>();

        public Liquidacao(int userId)
        {
            this.UserId = userId;
        }

        public LiquidacaoItem GetItem(dynamic virtualPath) 
            => items.FirstOrDefault(x => x.VirtualPath == virtualPath);

        public void AddItems(LiquidacaoItem item, IEventStore eventStore)
        {
            if (items.Add(item))
            {
                eventStore.Raise("LiquidacaoItem Added",
                    new { UserId, Length = item.Content.Length, item.VirtualPath });
            }
        }
    }

    public class LiquidacaoItem
    {
        public string VirtualPath { get; set; }
        public string Content { get; set; }

        public override bool Equals(object instance)
        {
            if (instance == null || GetType() != instance.GetType())
            {
                return false;
            }

            var obj = instance as LiquidacaoItem;
            return this.VirtualPath.Equals(obj.VirtualPath);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return this.VirtualPath.GetHashCode();
        }
    }
}