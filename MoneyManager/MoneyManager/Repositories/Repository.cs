using System.Linq;

namespace MoneyManager.DataAccessLayer.Repositories
{
    public abstract class Repository<T> where T : class
    {
        public ApplicationContext context;

        protected Repository(ApplicationContext context)
        {
            this.context = context;
        }

        public virtual IQueryable<T> GetAll()
        {
            return context.Set<T>()
                .AsQueryable();
        }

        public virtual void Create(T item)
        {
            context.Set<T>()
                .Add(item);
        }
        public virtual void Update(T item)
        {
            context.Set<T>()
                .Update(item);
        }

        public virtual void Delete(int id)
        {
            T item = context.Set<T>()
                .Find(id);
            if (item != null)
                context.Set<T>()
                    .Remove(item);
        }

        public virtual void Delete(T item)
        {
            if (item != null)
                context.Set<T>()
                    .Remove(item);
        }

        public T Get(int id)
        {
            return context.Set<T>()
                .Find(id);
        }

    }
}
