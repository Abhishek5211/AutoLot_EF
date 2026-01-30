using AutoLot.Dal.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Repos.Base
{
    public class CustomerOrderViewModelRepo : BaseViewRepo<CustomerOrderViewModel>, ICustomerOrderViewModelRepo
    {
        public CustomerOrderViewModelRepo(ApplicationDbContext context) : base(context)
        {
        }

        internal CustomerOrderViewModelRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
