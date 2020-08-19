using BD.DATA;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BD.REPO
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext context;
        public Repository(AppDbContext context)
        {
            this.context = context;
        }



        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public T Get(int id)
        {
            return context.Set<T>().SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().AsEnumerable();
        }

        public void Insert(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }


        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }


        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }


    }
}
