﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Data
{
    public class GenericRepository<T> : IGeneric<T> where T : class
    {
        private readonly AccountManagerContext _db;

        public GenericRepository(AccountManagerContext db) => _db = db;

        public async Task<List<T>> GetAll() => await _db.Set<T>().ToListAsync();

        public async Task<T> Find(Guid id) => await _db.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> expression) => 
            await _db.Set<T>().Where(expression).ToListAsync();

        public async Task<T> GetOneBy(Expression<Func<T, bool>> expression) => 
            await _db.Set<T>().FirstOrDefaultAsync(expression);

        public async Task Create(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }
    }
}
