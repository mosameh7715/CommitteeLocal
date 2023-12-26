﻿namespace Committees.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DBContext _dbContext;
        private IDbContextTransaction _transaction;
        public Guid _loggedInUserId;
        public UnitOfWork(DBContext dbContext,  IHttpContextAccessor _httpContextAccessor)
        {
            _dbContext = dbContext;
        }

        #region Transaction
        public void BeginTransaction() => _transaction = _dbContext.Database.BeginTransaction();
        public async Task BeginTransactionAsync() => _transaction = await _dbContext.Database.BeginTransactionAsync();
        public void Commit() => _transaction.Commit();
        public async Task CommitAsync() => await _transaction.CommitAsync();
        public void Rollback() => _transaction?.Rollback();
        public async Task RollbackAsync() => await _transaction.RollbackAsync();

        public void Dispose()
        {
            _transaction?.Dispose();
            _dbContext.Dispose();
        }
        public async Task DisposeAsync()
        {
            await _transaction.DisposeAsync();
            await _dbContext.DisposeAsync();
        }
        #endregion


        #region SaveChanges
        public void SaveChanges()
        {
            try
            {
                var entries = _dbContext.ChangeTracker.Entries()
                    .Where(e => (e.Entity is BaseEntity<Guid>) && (e.State == EntityState.Added || e.State == EntityState.Modified));

                foreach (var entityEntry in entries)
                {
                    if (entityEntry.State == EntityState.Modified)
                    {
                        ((BaseEntity<Guid>)entityEntry.Entity).UpdatedOn = DateTime.Now;
                        //((BaseEntity<Guid>)entityEntry.Entity).UpdatedBy = _loggedInUserId;
                        entityEntry.Property("CreatedOn").IsModified = false;
                        entityEntry.Property("CreatedBy").IsModified = false;
                    }

                    if (entityEntry.State == EntityState.Added)
                    {
                        ((BaseEntity<Guid>)entityEntry.Entity).CreatedOn = DateTime.Now;
                        //((BaseEntity<Guid>)entityEntry.Entity).CreatedBy = _loggedInUserId;
                    }

                }
                if (_dbContext.Database.CurrentTransaction == null)
                    BeginTransaction();
                _dbContext.SaveChanges();
                //Commit();
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;

            }

        }

        public async Task SaveChangesAsync()
        {
            try
            {

                var entries = _dbContext.ChangeTracker.Entries()
.Where(e => (e.Entity is BaseEntity<Guid>) && (
     e.State == EntityState.Added
     || e.State == EntityState.Modified));

                foreach (var entityEntry in entries)
                {
                    if (entityEntry.State == EntityState.Modified)
                    {
                        ((BaseEntity<Guid>)entityEntry.Entity).UpdatedOn = DateTime.Now;
                        ((BaseEntity<Guid>)entityEntry.Entity).UpdatedBy = _loggedInUserId;
                        entityEntry.Property("CreatedOn").IsModified = false;
                        entityEntry.Property("CreatedBy").IsModified = false;


                    }

                    if (entityEntry.State == EntityState.Added)
                    {
                        ((BaseEntity<Guid>)entityEntry.Entity).CreatedOn = DateTime.Now;
                        ((BaseEntity<Guid>)entityEntry.Entity).CreatedBy = _loggedInUserId;
                    }
                }
                if (_dbContext.Database.CurrentTransaction == null)
                    await BeginTransactionAsync();
                await _dbContext.SaveChangesAsync();
                //Commit();
            }
            catch (Exception)
            {
                await RollbackAsync();

            }

        }
        #endregion


    }
}
