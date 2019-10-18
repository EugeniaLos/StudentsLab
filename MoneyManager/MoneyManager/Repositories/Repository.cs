using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyManager
{
    public abstract class Repository<T> where T : class
    {
        public ApplicationContext context;

        protected Repository(ApplicationContext context)
        {
            this.context = context;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return context.Set<T>().AsEnumerable();
        }

        public virtual void Create(T item)
        {
            context.Set<T>().Add(item);
        }
        public virtual void Update(T item)
        {
            context.Set<T>().Update(item);
        }

        public virtual void Delete(int id)
        {
            T item = context.Set<T>().Find(id);
            if (item != null)
                context.Set<T>().Remove(item);
        }

        public virtual void Delete(T item)
        {
            if (item != null)
                context.Set<T>().Remove(item);
        }

        public T Get(int id)
        {
            return context.Set<T>().Find(id);
        }

    }
}
