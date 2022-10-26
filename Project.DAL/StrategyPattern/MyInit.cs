using Project.COMMON.Tools;
using Project.DAL.Context;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.StrategyPattern
{
    public class MyInit: CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            #region Admin

            AppUser admin = new AppUser();

            admin.UserName = "Eren";
            admin.Email = "erenbektas95@hotmail.com";
            admin.Password = Crypt.Decrypt("admin");
            admin.ConfirmPassword = Crypt.Decrypt("admin");
            admin.Role = ENTITIES.Enums.UserRole.Admin;
            admin.Active = true;
            context.AppUsers.Add(admin);
            context.SaveChanges();
            
           #endregion


        }
    }
}
