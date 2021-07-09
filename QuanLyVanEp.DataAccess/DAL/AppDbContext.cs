using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BaseApiWithIdentity.DataAccess.DAL.Entities.Identity;
using BaseApiWithIdentity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BaseApiWithIdentity.DataAccess.DAL.Entities;

namespace BaseApiWithIdentity.DataAccess.DAL
{
    public class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, int, UserClaimEntity, UserRoleEntity, 
        UserLoginEntity, RoleClaimEntity, UserTokenEntity>
    {
        public DbSet<LogEntity> Logs { get; set; }

        public AppDbContext([NotNull] DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData(builder);
            
            builder.Entity<UserRoleEntity>(x => {
                x.HasOne(a => a.User).WithMany(a => a.UserRoles).HasForeignKey(a => a.UserId);
                x.HasOne(a => a.Role).WithMany(a => a.UserRoles).HasForeignKey(a => a.RoleId);
            });

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                //// 1. Add the IsDeleted property
                //entityType.AddProperty("IsDeleted", typeof(bool)).SetDefaultValue(false);

                // 2. Create the query filter
                var parameter = Expression.Parameter(entityType.ClrType);

                // EF.Property<bool>(post, "IsDeleted")
                var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
                var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));

                // EF.Property<bool>(post, "IsDeleted") == false
                BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));

                // post => EF.Property<bool>(post, "IsDeleted") == false
                var lambda = Expression.Lambda(compareExpression, parameter);

                builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
            //SeedData(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateSoftDeleteStatuses();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            var entries = ChangeTracker.Entries();

            var countEntry = entries.Count();

            for (int i = 0; i < countEntry; i++)
            {
                var currentEntry = entries.FirstOrDefault();
                if (currentEntry.Entity.GetType() == typeof(UserEntity)) break;
                if (currentEntry == null) break;
                switch (currentEntry.State)
                {
                    case EntityState.Added:
                        currentEntry.CurrentValues["IsDeleted"] = false;
                        break;
                    case EntityState.Deleted:
                        currentEntry.State = EntityState.Modified;
                        currentEntry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }
        }

        public void SeedData(ModelBuilder builder)
        {
            builder.Entity<RoleEntity>().HasData(
                new RoleEntity
                {
                    Id = 1,
                    Name = AppRoles.Admin,
                    NormalizedName = AppRoles.Admin.ToUpper()
                },
                new RoleEntity
                {
                    Id = 2,
                    Name = AppRoles.User,
                    NormalizedName = AppRoles.User.ToUpper()
                }
            );
        }

        public void DetachAllEntities()
        {
            //Console.WriteLine("=============Detach all entity....===============");
            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
    }
}
