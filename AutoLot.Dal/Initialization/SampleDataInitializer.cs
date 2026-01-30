using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Initialization
{
    internal class SampleDataInitializer
    {

        internal static void DropAndCreateDatabase(ApplicationDbContext applicationDbContext)
        {
            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.Migrate();
        }

        internal static void SeedData(ApplicationDbContext context)
        {
            try
            {
                InsertDataEntity(context, context.Customers, SampleData.Customers);
                InsertDataEntity(context, context.Makes, SampleData.Makes);
                InsertDataEntity(context, context.Drivers, SampleData.Drivers);
                InsertDataEntity(context, context.Cars, SampleData.Inventory);
                InsertDataEntity(context, context.Radios, SampleData.Radios);
                InsertDataEntity(context, context.CarsToDrivers, SampleData.CarsAndDrivers);
                InsertDataEntity(context, context.Orders, SampleData.Orders);
                InsertDataEntity(context, context.CreditRisks, SampleData.CreditRisks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        internal static void InsertDataEntity<TEntity>(ApplicationDbContext applicationDbContext, DbSet<TEntity> table, List<TEntity> records) where TEntity : BaseEntity
        {
            if (table.Any())
                return;
            using var transaction = applicationDbContext.Database.BeginTransaction();
            try
            {
            var executionStrategy = applicationDbContext.Database.CreateExecutionStrategy();
                executionStrategy.Execute(() =>
                {
                    
                    //get entity metadata
                    var entity = applicationDbContext.Model.FindEntityType(typeof(TEntity).FullName);
                    applicationDbContext.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {entity.GetSchema()}.{entity.GetTableName()} ON");
                    table.AddRange(records);
                    applicationDbContext.SaveChanges();
                    applicationDbContext.Database.ExecuteSqlRaw(
                    $"SET IDENTITY_INSERT {entity.GetSchema()}.{entity.GetTableName()} OFF");
                    transaction.Commit();
                });
            }
            catch(Exception)  
            {
                transaction.Rollback();
            }

        }
        public static void InitializeData(ApplicationDbContext context)
        {
            DropAndCreateDatabase(context);
            SeedData(context);
        }
         
    }

}
