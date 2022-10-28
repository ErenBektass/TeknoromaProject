using Bogus.DataSets;
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
            AppUser au = new AppUser
            {
              UserName = "Eren",
              Email = "erenbektas95@hotmail.com",
              Password = Crypt.Decrypt("123"),
              ConfirmPassword = Crypt.Decrypt("123"),
              Role = ENTITIES.Enums.UserRole.Admin,
              Active = true           
            };
            context.AppUsers.Add(au);
            context.SaveChanges();

            //Projedeki Kişileri Tanımlama
            AppUserProfile aup = new AppUserProfile
            {
                ID = au.ID,
                FirstName = "Eren",
                LastName = "Bektas",
                TCNO = "12312412",
                Age = 27,
                Gender = ENTITIES.Enums.Gender.Male
            };           
            context.Profiles.Add(aup);
            context.SaveChanges();
            #endregion

            #region User
            AppUser user = new AppUser
            {
                UserName = "User",
                Password = Crypt.Decrypt("2322"),
                ConfirmPassword = Crypt.Decrypt("2322"),
                Email = "erenbektas95@hotmail.com",
                Role = ENTITIES.Enums.UserRole.Member,
                Active = true
            };
            
            context.AppUsers.Add(user);
            context.SaveChanges();

            AppUserProfile up = new AppUserProfile();
            up.ID = up.ID;
            up.FirstName = "Hasan";
            up.LastName = "Topcu";
            up.TCNO = "21312322";
            up.Age = 55;
            up.Gender = ENTITIES.Enums.Gender.Male;
            context.Profiles.Add(up);
            context.SaveChanges();
            #endregion

            #region BranchManager
            AppUser bm = new AppUser
            {
                UserName = "manager",
                Password = Crypt.Decrypt("222"),
                ConfirmPassword = Crypt.Decrypt("222"),
                Email = "erenbektas95@hotmail.com",
                Role = ENTITIES.Enums.UserRole.BranchManager,
                Active = true
            };
             context.AppUsers.Add(bm);
             context.SaveChanges();

            Employee manager = new Employee
            {
                FirstName = "Haluk",
                LastName="Saygın",
                Email= "erenbektas95@hotmail.com",
                PhoneNumber="055333333333",
                Gender=ENTITIES.Enums.Gender.Male,
                Salary=3000
            };
            context.Employees.Add(manager);
            context.SaveChanges();
            #endregion

            #region SalesRepresentative
            AppUser sales = new AppUser
            {
                UserName="Sales",
                Password=Crypt.Decrypt("333"),
                ConfirmPassword=Crypt.Decrypt("333"),
                Email="erenbektas95@hotmail.com",
                Role=ENTITIES.Enums.UserRole.SalesRepresentative,
                Active=true
            };
            context.AppUsers.Add(sales);
            context.SaveChanges();

            Employee sls = new Employee
            {
                FirstName="Gül",
                LastName="Satar",
                TCNO="2111111111",
                PhoneNumber="05463332312",
                Gender=ENTITIES.Enums.Gender.Woman,
                MonthlySales=23000,
                Salary=5500              
            };
            context.Employees.Add(sls);
            context.SaveChanges();
            #endregion

            #region MobileSalesRepresentative
            AppUser msp = new AppUser
            {
                UserName="Mobile",
                Password=Crypt.Decrypt("9999"),
                ConfirmPassword=Crypt.Decrypt("9999"),
                Email="erenbektas95@hotmail.com",
                Role=ENTITIES.Enums.UserRole.MobileSalesRepresentative,
                Active=true
            };
            context.AppUsers.Add(msp);
            context.SaveChanges();
            Employee mobile = new Employee
            {
                FirstName="Fahri",
                LastName="cepçi",
                TCNO="8888888888",
                PhoneNumber="05875256568",
                Gender=ENTITIES.Enums.Gender.Male,
                MonthlySales=25000,
                Salary=7750
            };
            context.Employees.Add(mobile);
            context.SaveChanges();

            #endregion

            #region WarehouseRepresentative
            AppUser wh = new AppUser
            {
                UserName="ware",
                Password=Crypt.Decrypt("5555"),
                ConfirmPassword=Crypt.Decrypt("5555"),
                Email="erenbektas95@hotmail.com",
                Role=ENTITIES.Enums.UserRole.WarehouseRepresentative,
                Active=true
            };
            context.AppUsers.Add(wh);
            context.SaveChanges();
            Employee ware = new Employee
            {
                FirstName="Kerim",
                LastName="Zulacı",
                TCNO="333333333",
                PhoneNumber="05554443321",
                Gender=ENTITIES.Enums.Gender.Male,
                MonthlySales=18500,
                Salary=9000               
            };
            context.Employees.Add(ware);
            context.SaveChanges();
            #endregion

            #region AccountingRepresentative
            AppUser ar = new AppUser
            {
                UserName = "accounting",
                Password = Crypt.Decrypt("88888"),
                ConfirmPassword = Crypt.Decrypt("88888"),
                Email = "erenbektas95@hotmail.com",
                Role = ENTITIES.Enums.UserRole.AccountingRepresentative,
                Active = true
            };
            context.AppUsers.Add(ar);
            context.SaveChanges();
            Employee accouting = new Employee
            {
                FirstName="Feyza",
                LastName="Paragöz",
                TCNO="666666666",
                PhoneNumber="0554555444231",
                Gender=ENTITIES.Enums.Gender.Woman,
                MonthlySales=13500,
                Salary=5900
            };
            context.Employees.Add(accouting);
            context.SaveChanges();
            #endregion

            #region TechnicalServiceRepresentative
            AppUser tsr = new AppUser
            {
                UserName = "technical",
                Password = Crypt.Decrypt("6666"),
                ConfirmPassword = Crypt.Decrypt("6666"),
                Email = "erenbektas95@hotmail.com",
                Role = ENTITIES.Enums.UserRole.TechnicalServiceRepresentative,
                Active = true
            };
            context.AppUsers.Add(tsr);
            context.SaveChanges();
            Employee technical = new Employee
            {
                FirstName="Özgün",
                LastName="Kablocu",
                PhoneNumber="05543658598",
                Gender=ENTITIES.Enums.Gender.Male,
                MonthlySales=12500,
                Salary=12500               
            };
            context.Employees.Add(technical);
            context.SaveChanges();
            #endregion

            #region Fake Data
            //KategoriVeUrunBilgileri
            for (int i = 0; i < 10; i++)
            {
                Category c = new Category
                {
                    CategoryName = new Commerce("tr").Categories(1)[0],
                    Description = new Lorem("tr").Sentence(10)
                };
                

                for (int j = 0; j < 30; j++)
                {
                    Product p = new Product
                    {
                        ProductName = new Commerce("tr").ProductName(),
                        UnitPrice = Convert.ToDecimal(new Commerce("tr").Price()),
                        UnitsInStock = 100,
                        ImagePath = new Images().LoremPixelUrl(),
                        
                    };
                    c.Products.Add(p);
                    
                }
                context.Categories.Add(c);
                context.SaveChanges();
            }
            #endregion







        }
    }
}
