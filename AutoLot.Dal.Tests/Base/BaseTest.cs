using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Tests.Base
{
    public abstract class BaseTest : IDisposable
    {
        protected readonly IConfiguration Configuration;
        protected readonly ApplicationDbContext Context;
        protected readonly ITestOutputHelper OutputHelper;
        protected BaseTest(ITestOutputHelper outputHelper)
        {
            Configuration = TestHelper.GetConfiguration();
            Context = TestHelper.GetContext(Configuration);
            OutputHelper = outputHelper;
        }

        public virtual void Dispose()
        {
            Context.Dispose();
        }

        protected void ExecuteInATransaction(Action action)
        {
            var strategy = Context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using var trans = Context.Database.BeginTransaction();
                action();
                trans.Rollback();
            });
        }

        protected void ExecuteInASharedTransaction(Action<IDbContextTransaction> action)
        {
            var strategy = Context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using var trans = Context.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
                action(trans);
                trans.Rollback();
            });
        }

    }
}
