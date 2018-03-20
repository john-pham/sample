// ======================================
// Author: Ebrain Team
// Email:  info@ebrain.com.vn
// Copyright (c) 2017 www.ebrain.com.vn
// 
// ==> Contact Us: contact@ebrain.com.vn
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //void Add(TEntity entity);
        //void AddRange(IEnumerable<TEntity> entities);

        //void Update(TEntity entity);
        //void UpdateRange(IEnumerable<TEntity> entities);

        //void Remove(TEntity entity);
        //void RemoveRange(IEnumerable<TEntity> entities);

        //int Count();

        //IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        //TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        //TEntity Get(Guid id);
        //IEnumerable<TEntity> GetAll();

        Task<bool> Execute(string command, params object[] parameters);
    }

}
