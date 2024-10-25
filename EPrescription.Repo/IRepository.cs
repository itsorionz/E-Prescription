using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EPrescription.Entities;

namespace EPrescription.Repo
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        void Add(T item);

        IEnumerable<T> GetAll();

        T GetById(int id);

        void DeleteById(int id);

        void DeleteByItem(T item);
        
    }

    public interface IRepository<T>:IBaseRepository<T> where T : Entity
    {
        int GetCount();
        int GetCount(DateTime date);
        int GetCount(int year);
    }

}