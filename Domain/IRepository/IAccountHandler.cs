namespace Liquidacoes.Domain.IRepository
{
    using System.Collections.Generic;

    internal interface IAccountHandler<T, in K>
        where T : class
        where K : struct
    {
        void Create(T entity);
        void CreateRange(List<T> entity);
        void Remove(T entity);
        void RemoveRange(List<T> entity);
        void Update(T entity);
        ICollection<T> FindAll();
        T Find(K guid);
        bool Save();
    }
}