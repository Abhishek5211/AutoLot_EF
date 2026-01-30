using AutoLot.Dal.Repos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Tests.IntegrationTests
{
    [Collection("Integration Tests")]
    public class CustomerOrderViewModelTests : BaseTest, IClassFixture<EnsureAutoLotDatabaseTestFixture>
    {
        private readonly ICustomerOrderViewModelRepo _repo;
        public CustomerOrderViewModelTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _repo = new CustomerOrderViewModelRepo(Context);
        }
        public override void Dispose()
        {
            _repo.Dispose();
            base.Dispose();
        }

        [Fact]
        public void ShouldGetAllViewModels()
        {
            var qs = Context.CustomerOrderViewModels.ToQueryString();
            OutputHelper.WriteLine($"Query: {qs}");
            List<Models.ViewModels.CustomerOrderViewModel> list = Context.CustomerOrderViewModels.ToList();
            Assert.NotEmpty(list);
            Assert.Equal(5, list.Count());

        }
    }
}
