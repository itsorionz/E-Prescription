using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EPrescription.Entities;

namespace EPrescription.Repo
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private DbContext _context ;
        public BaseRepository(DbContext context)
        {
            _context = context;
        }
     
        public virtual void Add(T item)
        {
            _context.Set<T>().Add(item);
        }
        public virtual void Update(T entityToUpdate)
        {
            _context.Set<T>().Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual T GetById(int id)
        {
            //return _context.Set<T>().Where(x => x.Id == id).FirstOrDefault();
            return _context.Set<T>().Find(id);
        }
        public virtual void DeleteById(int id)
        {
            T item = GetById(id);
            DeleteByItem(item);
        }
        public virtual void DeleteByItem(T item)
        {
            item.StatusFlag = 0;
        }
        public virtual void DeleteFromDatabaseById(int id)
        {
            var item = GetById(id);
            DeleteFromDatabaseByItem(item);
        }
        public virtual void DeleteFromDatabaseByItem(T item)
        {
            _context.Set<T>().Remove(item);
        }
    }

    public class Repository<T> : BaseRepository<T>, IRepository<T> where T : Entity
    {
        private DbContext _context ;
        public Repository(DbContext context)
            : base(context)
        {
           _context = context;
        }

        public virtual int GetCount()
        {
            return  _context.Set<T>().Count();
        }
        public virtual int GetCount(DateTime date)
        {
            return _context.Set<T>().Count(e => e.CreatedAt.Value.Year == date.Year && e.CreatedAt.Value.Month == date.Month && e.CreatedAt.Value.Day == date.Day);
        }
        public virtual int GetCount(int year)
        {
            return _context.Set<T>().Count(e => e.CreatedAt.Value.Year == year);
        }
        public virtual string GenerateRequisitionNo(int? formId)
        {
            string fmt = "0000.##";
            return string.Format("{0:yyMMdd}", DateTime.Now) + formId +
                   (GetCount(DateTime.Now.Year) + 1).ToString(fmt);
        }
    }
}
