﻿using System.Linq.Expressions;
using System.Numerics;
using EMS.Domain.Abstraction;
using EMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace EMS.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly EMSDbContext _context;
        private readonly DbSet<TEntity> _entities;

        public Repository(EMSDbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public async Task<int> CountAsync(
            Expression<Func<TEntity, bool>> pridecate = null!,
            CancellationToken cancellationToken = default
        )
        {
            if (pridecate == null)
                return await _entities.CountAsync(cancellationToken);

            return await _entities.CountAsync(pridecate, cancellationToken);
        }

        public ValueTask<EntityEntry<TEntity>> CreateAsync(
            TEntity entity,
            CancellationToken cancellationToken = default
        ) => _entities.AddAsync(entity, cancellationToken);

        public ValueTask<EntityEntry<TEntity>> DeleteAsync(
            TEntity entity,
            CancellationToken cancellationToken = default
        ) => ValueTask.FromResult(_entities.Remove(entity));

        public async Task<IReadOnlyCollection<TSelctor>> GetAllAsync<TSelctor>(
            Expression<Func<TEntity, TSelctor>> Selctor,
            Expression<Func<TEntity, bool>> predicate = default!,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null!,
            CancellationToken cancellationToken = default
        )
        {
            var query = _entities.AsQueryable().AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (includes != null)
                query = includes(query);

            return await query.Select(Selctor).ToListAsync(cancellationToken);
        }

        public Task<TSelctor> GetAsync<TSelctor>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TSelctor>> Selctor,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null!,
            bool astracking = true,
            CancellationToken cancellationToken = default
        )
        {
            var query = _entities.AsQueryable();

            if (!astracking)
                query = query.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (includes != null)
                query = includes(query);

            return query.Select(Selctor).FirstAsync(cancellationToken);
        }

        public Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            bool astracking = true,
            CancellationToken cancellationToken = default
        )
        {
            var query = _entities.AsQueryable();

            if (!astracking)
                query = query.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (includes != null)
                query = includes(query);

            return query.FirstAsync(cancellationToken);
        }

        public async Task<bool> IsAnyExistAsync(
            Expression<Func<TEntity, bool>> pridecate,
            CancellationToken cancellationToken = default
        ) => await _entities.AnyAsync(pridecate, cancellationToken);

        public async Task<IReadOnlyCollection<TSelctor>> PaginateAsync<TSelctor>(
            int page,
            int pageSize,
            Expression<Func<TEntity, TSelctor>> Selctor,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            CancellationToken cancellationToken = default
        )
        {
            var query = _entities.AsQueryable().AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (includes != null)
                query = includes(query);

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return await query.Select(Selctor).ToListAsync(cancellationToken);
        }

        public ValueTask<EntityEntry<TEntity>> UpdateAsync(
            TEntity entity,
            CancellationToken cancellationToken = default
        ) => ValueTask.FromResult(_entities.Update(entity));
    }
}
