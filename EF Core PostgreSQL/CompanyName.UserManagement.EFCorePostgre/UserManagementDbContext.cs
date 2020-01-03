using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CompanyName.UserManagement.EFCorePostgre.Entity;
using CompanyName.UserManagement.EFCorePostgre.EntityConfiguration;

namespace CompanyName.UserManagement.EFCorePostgre
{
    public class UserManagementDbContext : DbContext
    {
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Realm> Realms { get; set; }
        public DbSet<RealmSolution> RealmSolution { get; set; }
        //public DbSet<Orgnization> Orgnizations { get; set; }
        //public DbSet<Role> Roles { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Capital> Capitals { get; set; }

        public UserManagementDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RealmConfiguration());
            modelBuilder.ApplyConfiguration(new SolutionConfiguration());
            modelBuilder.ApplyConfiguration(new RealmSolutionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType == null) continue;

                // lowercase table name & RemovePluralizingTableName
                entityType.Relational().TableName = entityType.ClrType.Name.ToLower();

                //lowercase primary key name
                var key = entityType.FindPrimaryKey();
                if (key != null)
                {
                    key.Relational().Name = key.Relational().Name.ToLower();
                }

                //lowercase column name
                foreach (var property in entityType.GetProperties())
                {
                    property.Relational().ColumnName = property.Relational().ColumnName.ToLower();
                }

                //lowercase ForeignKeys
                foreach (var property in entityType.GetProperties())
                {
                    foreach (var fk in entityType.FindForeignKeys(property))
                    {
                        fk.Relational().Name = fk.Relational().Name.ToLower();
                    }
                }

                //rename index
                foreach (var index in entityType.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name.Replace("[", string.Empty).Replace("]", string.Empty).ToLower();
                }
            }           
        }
    }
}
