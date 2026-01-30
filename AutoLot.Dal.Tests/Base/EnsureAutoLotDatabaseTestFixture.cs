namespace AutoLot.Dal.Tests.Base;

public sealed class EnsureAutoLotDatabaseTestFixture : IDisposable
{
    public EnsureAutoLotDatabaseTestFixture()
    {
        var configuration = TestHelper.GetConfiguration();
        var context = TestHelper.GetContext(configuration);
        SampleDataInitializer.InitializeData(context);
        context.Dispose();
    }

    public void Dispose()
    {
    }
}
