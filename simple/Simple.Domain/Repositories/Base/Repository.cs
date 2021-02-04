using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Domain.Repositories
{
    public class Repository<TEntity> : Repository<TEntity, int>, IRepository<TEntity>
         where TEntity : class, IEntity
    {
        public Repository(DbContext context) : base(context)
        {
        }
    }

    public class Repository<TEntity, TPrimaryKey> : BaseRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly DbContext _db;
        public virtual DbSet<TEntity> _table => _db.Set<TEntity>();

        public Repository(DbContext context)
        {
            _db = context;
        }

        #region Query

        public override IQueryable<TEntity> Query()
        {
            return _table.AsQueryable();
        }

        public override IQueryable<TEntity> QueryNoTracking()
        {
            return _table.AsQueryable().AsNoTracking();
        }

        #endregion Query

        #region Insert

        public override TEntity Insert(TEntity entity)
        {
            var newEntity = _table.Add(entity).Entity;
            return newEntity;
        }

        public override async Task<TEntity> InsertAsync(TEntity entity)
        {
            var entityEntry = await _table.AddAsync(entity);
            return entityEntry.Entity;
        }

        public override void Insert(List<TEntity> entities)
        {
            _table.AddRange(entities);
        }

        public override Task InsertAsync(List<TEntity> entities)
        {
            _table.AddRangeAsync(entities);
            return Task.CompletedTask;
        }

        #endregion Insert

        #region Update

        public override TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            _db.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        #endregion Update

        #region Delete

        public override void Delete(TEntity entity)
        {
            if (entity != null)
            {
                entity.IsDelete = true;
                entity.DeleteTime = DateTime.Now;
                Update(entity);
            }
        }

        public override void Delete(TPrimaryKey id)
        {
            var entity = Get(id);
            Delete(entity);
        }

        public override void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = GetAll(predicate);
            if (entities.Any())
            {
                entities.ForEach(entity =>
                {
                    Delete(entity);
                });
            }
        }

        #endregion Delete

        #region HardDelete

        public override void HardDelete(TEntity entity)
        {
            AttachIfNot(entity);
            _table.Remove(entity);
        }

        public override void HardDelete(TPrimaryKey id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                HardDelete(entity);
                return;
            }

            entity = Get(id);
            if (entity != null)
            {
                HardDelete(entity);
                return;
            }
        }

        public override void HardDelete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _table.Where(predicate).ToList();
            if (entities.Any())
            {
                entities.ForEach(entity =>
                {
                    AttachIfNot(entity);
                });
                _table.RemoveRange(entities);
            }
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = _db.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            _table.Attach(entity);
        }

        private TEntity GetFromChangeTrackerOrNull(TPrimaryKey id)
        {
            var entry = _db.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<TPrimaryKey>.Default.Equals(id, ((TEntity)ent.Entity).Id)
                );

            return entry?.Entity as TEntity;
        }

        #endregion HardDelete
    }
}
