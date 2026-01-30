using AutoLot.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Tests.IntegrationTests
{
    [Collection("Integration Tests")]
    public class CustomerTests : BaseTest, IClassFixture<EnsureAutoLotDatabaseTestFixture>
    {
        public CustomerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public void ShouldGetAllOfTheCustomers()
        {
            var qs = Context.Customers.ToQueryString();
            var customers = Context.Customers.ToList();
            Assert.Equal(5, customers.Count);
        }


        [Fact]
        public void ShouldGetCustomersWithLastNameW()
        {
            IQueryable<Customer> query = Context.Customers.Where(x => x.PersonalInformation.LastName.StartsWith("W"));
            var qs = query.ToQueryString();
            OutputHelper.WriteLine($"Query : {qs}");
            List<Customer> customers = query.ToList();
            Assert.Equal(2, customers.Count());
            foreach (var customer in customers)
            {
                var pi = customer.PersonalInformation;
                Assert.StartsWith("W", pi.LastName, StringComparison.OrdinalIgnoreCase);
            }
        }

        [Fact]
        public void ShouldGetCustomersWithLastNameWOrH()
        {
            IQueryable<Customer> query = Context.Customers
            .Where(x => x.PersonalInformation.LastName.StartsWith("W") ||
            x.PersonalInformation.LastName.StartsWith("H"));
            var qs = query.ToQueryString();
            OutputHelper.WriteLine($"Query: {qs}");
            List<Customer> customers = query.ToList();
            Assert.Equal(3, customers.Count);
            foreach (var customer in customers)
            {
                var pi = customer.PersonalInformation;
                Assert.True(
                pi.LastName.StartsWith("W", StringComparison.OrdinalIgnoreCase) ||
                pi.LastName.StartsWith("H", StringComparison.OrdinalIgnoreCase));
            }
        }

        [Fact]
        public void ShouldGetCustomersWithLastNameWAndFirstNameM()
        {
            IQueryable<Customer> query = Context.Customers
            .Where(x => x.PersonalInformation.LastName.StartsWith("W") &&
            x.PersonalInformation.FirstName.StartsWith("M"));
            var qs = query.ToQueryString();
            OutputHelper.WriteLine($"Query: {qs}");
            List<Customer> customers = query.ToList();
            Assert.Single(customers);
            foreach (var customer in customers)
            {
                var pi = customer.PersonalInformation;
                Assert.StartsWith("W", pi.LastName, StringComparison.OrdinalIgnoreCase);
                Assert.StartsWith("M", pi.FirstName, StringComparison.OrdinalIgnoreCase);
            }
        }


        [Fact]
        public void ShouldGetCustomersWIthLastNameWorH()
        {
            IQueryable<Customer> query = Context.Customers.Where(x => EF.Functions.Like(x.PersonalInformation.LastName, "W%") ||
                                    EF.Functions.Like(x.PersonalInformation.LastName, "H%"));
            var qs = query.ToQueryString();
            OutputHelper.WriteLine($"Query: {qs}");
            List<Customer> customers = query.ToList();
            Assert.Equal(3, customers.Count);
        }

        [Fact]
        public void ShouldSortByLastNameThenFirstName()
        {

            var query = Context.Customers.OrderBy(x => x.PersonalInformation.LastName).ThenByDescending(x => x.PersonalInformation.FirstName);

            var qs = query.ToQueryString();
            OutputHelper.WriteLine($"Query : {qs}");
            var customers = query.ToList();
            if (customers.Count <= 1) { return; }
            for (int x = 0; x < customers.Count - 1; x++)
            {
                Compare(customers[x].PersonalInformation, customers[x + 1].PersonalInformation);
            }

            static void Compare(Person p1, Person p2)
            {
                var compareVal = string.Compare(p1.LastName, p2.LastName, StringComparison.CurrentCultureIgnoreCase);
                Assert.True(compareVal <= 0);
                if (compareVal == 0)
                {
                    Assert.True(string.Compare(p1.FirstName, p2.FirstName, StringComparison.CurrentCultureIgnoreCase) >= 0);
                }
            }




        }


        [Fact]
        public void ShouldSortByFirstNameThenLastUsingReverse()
        {
            var query = Context.Customers.OrderBy(x => x.PersonalInformation.FirstName).ThenByDescending(x => x.PersonalInformation.LastName).Reverse();
            var qs = query.ToQueryString();
            var customers = query.ToList();
            if (customers.Count <= 1) { return; }
            for (int i = 0; i < customers.Count - 1; i++)
            {
                var p1 = customers[i].PersonalInformation;
                var p2 = customers[i].PersonalInformation;
                var compareLastName = string.Compare(p1.LastName, p2.LastName, StringComparison.CurrentCultureIgnoreCase);
                Assert.True(compareLastName >= 0);
                if (compareLastName != 0) continue;
                var compareFirstName = string.Compare(p1.FirstName, p2.FirstName, StringComparison.CurrentCultureIgnoreCase);
                Assert.True(compareFirstName <= 0);


            }
        }

        [Fact]
        public void GetFirstMatchingRecordDatabaseOrder()
        {
            //Gets the first record, database order
            var customer = Context.Customers.First();
            Assert.Equal(1, customer.Id);
        }


        [Fact]
        public void GetFirstMatchingRecordNameOrder()
        {
            var customer = Context.Customers
            .OrderBy(x => x.PersonalInformation.LastName)
            .ThenBy(x => x.PersonalInformation.FirstName)
            .First();
            Assert.Equal(1, customer.Id);
        }

        [Fact]
        public void FirstShouldThrowExceptionIfNoneMatch()
        {
            Assert.Throws<InvalidOperationException>(() => Context.Customers.First(x => x.Id == 10));
        }


        [Fact]
        public void FirstOrDefaultShouldReturnDefaultIfNoneMatch()
        {
            Expression<Func<Customer, bool>> expression = x => x.Id == 10;
            var customer = Context.Customers.FirstOrDefault(expression);
            Assert.Null(customer);
        }

        [Fact]
        public void GetLastMatchingRecordNameOrder()
        {
            //Gets the last record, lastname desc, first name desc order
            var customer = Context.Customers
            .OrderBy(x => x.PersonalInformation.LastName)
            .ThenBy(x => x.PersonalInformation.FirstName)
            .Last();
            Assert.Equal(4, customer.Id);
        }

        [Fact]
        public void LastShouldThrowIfNoSortSpecified()
        {
            Assert.Throws<InvalidOperationException>(() => Context.Customers.Last());
        }

        [Fact]
        public void GetOneMatchingRecordWithSingle()
        {
            var customer = Context.Customers.Single(x => x.Id == 1);
            Assert.Equal(1, customer.Id);
        }

        [Fact]
        public void SingleShouldThrowExceptionIfNoneMatch()
        {
            //Filters based on Id. Throws due to no match
            Assert.Throws<InvalidOperationException>(() => Context.Customers.Single(x => x.Id == 10));
        }

        [Fact]
        public void SingleShouldThrowExceptionIfMoreThenOneMatch()
        {
            // Throws due to more than one match
            Assert.Throws<InvalidOperationException>(() => Context.Customers.Single());
        }

        [Fact]
        public void SingleOrDefaultShouldThrowExceptionIfMoreThenOneMatch()
        {
            Assert.Throws<InvalidOperationException>(() => Context.Customers.SingleOrDefault());
        }

        [Fact]
        public void SingleOrDefaultShouldReturnDefaultIfNoneMatch()
        {
            Expression<Func<Customer, bool>> ex = x => x.Id == 10;
            var customer = Context.Customers.SingleOrDefault(ex);
            Assert.Null(customer);
        }
    }
}