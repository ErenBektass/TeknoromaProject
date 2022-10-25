using Project.DAL.StrategyPattern;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Context
{
    public class MyContext:DbContext
    {
        public MyContext():base ("MyConnection")
        {
            Database.SetInitializer(new MyInit());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}
