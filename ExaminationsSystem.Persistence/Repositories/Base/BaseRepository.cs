using ExaminationsSystem.Application.Common.Models;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories.Base;
using ExaminationsSystem.Domain.Common.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ExaminationsSystem.Persistence.Repositories.Base
{
    public class BaseRepository<TEntity, TPrimeryKey> :
        IDisposable,
        IBaseRepositoryAsync<TEntity, TPrimeryKey>
        where TEntity : class, IEntityIdentity<TPrimeryKey>
    {
        protected readonly ExaminationsSystemDbContext _context;
        protected DbSet<TEntity> Entities { get; set; }
        public BaseRepository(ExaminationsSystemDbContext dbContext)
        {
            _context = dbContext;
            this.Entities = this._context.Set<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity is ICreationDateSignature)
            {
                DateTime now = DateTime.Now;
                var dateTimeSignature = (ICreationDateSignature)entity;
                dateTimeSignature.CreationDate = now;
            }
            await this.Entities.AddAsync(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            if (entity is IDeletionSignature signature)
            {
                DateTime now = DateTime.Now;
                var virtualDeleteEntity = signature;

                if (virtualDeleteEntity.MustDeletedPhysical == true)
                {
                    this.Entities.Remove(entity);
                }
                else
                {
                    virtualDeleteEntity.IsDeleted = true;
                    virtualDeleteEntity.DeletionDate = now;
                    this.Entities.Update(entity);
                }
            }
            else
            {
                this.Entities.Remove(entity);
            }
        }
        public async Task Delete(TPrimeryKey id)
        {
            var entity = await this.Entities.FindAsync(id);
            this.Delete(entity);
        }

        public IQueryable<TEntity> Get()
        {
            var result = this.Entities.AsQueryable();
            return result;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            var result = this.Entities.AsQueryable();
            if (predicate != null)
            {
                result = result.Where(predicate);
            }
            return result;
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => Entities.AnyAsync(predicate, cancellationToken);

        public async Task<TEntity> GetByIdAsync(TPrimeryKey id)
        {
            var result = await this.Entities.FindAsync(id);
            return result;
        }

        public async Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.Entities.CountAsync(predicate);
        }

        public IQueryable<TEntity> SetPagination(IQueryable<TEntity> source, Pagination pagination)
        {
            if (source != null &&
                pagination != null)
            {
                source = source.Skip(pagination.PageIndex * pagination.PageSize)
                               .Take(pagination.PageSize);
            }
            return source;
        }

        public async Task<Pagination> SetPaginationCount(IQueryable<TEntity> source, Pagination pagination)
        {
            if (pagination != null &&
                pagination.GetTotalCount)
            {
                pagination.TotalCount = await source.LongCountAsync();
            }

            return pagination;
        }
        public IQueryable<TEntity> SetSortOrder(IQueryable<TEntity> source, string sortOrder)
        {
            if (source != null &&
                string.IsNullOrEmpty(sortOrder) == false &&
                sortOrder != "null")
            {
                source = source.OrderBy(sortOrder);
            }

            return source;
        }
        public TEntity Update(TEntity entity)
        {
            if (entity is IModificatioDateSignature)
            {
                DateTime now = DateTime.Now;
                var dateTimeSignature = (IModificatioDateSignature)entity;

                if (dateTimeSignature.FirstModificationDate.HasValue == false)
                    dateTimeSignature.FirstModificationDate = now;
                else
                    dateTimeSignature.LastModificationDate = now;
            }
            this.Entities.Update(entity);
            return entity;
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
