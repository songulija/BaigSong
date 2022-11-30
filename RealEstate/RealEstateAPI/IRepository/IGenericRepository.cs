using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RealEstateAPI.IRepository
{
    /// <summary>
    /// In this interface we define methods, basic functionality
    /// that will be shared accross our tables and database operations
    /// <T> means that its prepared to take any type of class that is send to here
    /// </summary>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// first function is get all. When dealing with ASYNC programming we use Task
        /// IList of type T (generic). GetAll method has parameter which is of type expression (function)
        /// its optional parameter. Next it also takes IQueryable of type T IOrderedQueryable, orderBy is optional also
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// this one is just getting One record. its taking same expression parameter
        /// and it will have includes. returns object(t)
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<T> Get(Expression<Func<T, bool>> expression = null,
            string includeProperties = "");

        //method to insert one entity(object) to database
        Task Insert(T entity);
        //method that takes list of entities and inserts to database
        Task InsertRange(IEnumerable<T> entities);
        //delete method takes id
        Task Delete(int id);
        //method takes list of entities that we want to delete. its void operation
        void DeleteRange(IEnumerable<T> entities);
        //method to update row in database. its void operation
        void Update(T entity);


    }
}
