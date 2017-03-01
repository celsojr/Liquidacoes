namespace Liquidacoes.Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;
    using Liquidacoes.Handlers.Common;
    using Liquidacoes.Domain.IRepository;

    internal abstract class AccountHandler<T, U>
        : Handler<AccountHandler<T, U>>, IAccountHandler<T, U>
        where T : class
        where U : struct
    {
        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void CreateRange(List<T> entity)
        {
            throw new NotImplementedException();
        }

        public T Find(U guid)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(List<T> entity)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        protected virtual T Initialize(T TClass = null, params object[] @params) => (T)TClass;

    }
}
