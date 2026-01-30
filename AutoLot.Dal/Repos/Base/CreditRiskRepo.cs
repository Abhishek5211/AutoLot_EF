using AutoLot.Dal.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Dal.Repos.Base
{
    public class CreditRiskRepo : TemporalTableBaseRepo<CreditRisk>, ICreditRiskRepo
    {
        public CreditRiskRepo(ApplicationDbContext context) : base(context)
        {
        }

        internal CreditRiskRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
