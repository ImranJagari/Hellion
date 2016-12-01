using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Hellion.Core.Database.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private static object syncDbContext = new object();

        protected DbContext dbContext;
        protected DbSet<T> dbSet;

        /// <summary>
        /// Creates a new Repository base instance.
        /// </summary>
        /// <param name="context">Database context</param>
        public RepositoryBase(DbContext context)
        {
            this.dbContext = context;
            this.dbSet = this.dbContext.Set<T>();
        }

        /// <summary>
        /// Add a new entity to the database.
        /// </summary>
        /// <param name="value">Entity value</param>
        public virtual void Add(T value)
        {
            lock (syncDbContext)
            {
                this.dbSet.Add(value);
                //value = this.dbSet.Add(value).Entity;
                this.Save();
            }
        }

        /// <summary>
        /// Deletes an entity by his id.
        /// </summary>
        /// <param name="id">Entity id</param>
        public virtual void Delete(int id)
        {
            lock (syncDbContext)
            {
                var entity = this.dbSet.Find(id);

                if (entity != null)
                    this.Delete(entity);
            }
        }

        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <param name="value">Entity value</param>
        public virtual void Delete(T value)
        {
            lock (syncDbContext)
            {
                this.dbSet.Remove(value);
                this.Save();
            }
        }

        /// <summary>
        /// Gets an entity from an expression.
        /// </summary>
        /// <param name="expression">Expression</param>
        /// <returns></returns>
        public virtual T Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> set = this.dbSet;

            foreach (var include in includes)
                set = this.dbSet.Include(include);

            return set.Where(expression).FirstOrDefault();
        }

        /// <summary>
        /// Get all entities from the database.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> set = this.dbSet;

            foreach (var include in includes)
                set = this.dbSet.Include(include);

            return set;
        }

        /// <summary>
        /// Update an existing entity.
        /// </summary>
        /// <param name="value">Entity</param>
        public virtual void Update(T value)
        {
            lock (syncDbContext)
            {
                value = this.dbSet.Update(value).Entity;
                this.Save();
            }
        }

        /// <summary>
        /// Save the database changes.
        /// </summary>
        protected void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
