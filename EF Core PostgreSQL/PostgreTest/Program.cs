using CompanyName.UserManagement.EFCorePostgre;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using CompanyName.UserManagement.EFCorePostgre.Entity;
using System.Linq;
using System.Collections.Generic;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions;
namespace PostgreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            var optionsBuilder = new DbContextOptionsBuilder<UserManagementDbContext>();
            //optionsBuilder.UseNpgsql("Server=10.137.114.199;Database=UserManagement;uid=postgres;pwd=CompanyName@2018");
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Database=AdminSite;uid=sa;pwd=sa");
            using (UserManagementDbContext context = new UserManagementDbContext(optionsBuilder.Options))
            {
                City ci = new City() { Id = "1", CityName = "cn" };
                Capital c = new Capital() { City = ci, other = "2" };
                context.Add(c);
                //Realm r = new Realm();
                //r.RlmId = 1;
                //r.RealmName = "Realm1";
                //context.Add<Realm>(r);
                Realm realm1 =  context.Realms.Where(i => EF.Functions.ILike(i.RealmName, "r%")).FirstOrDefault();
                Realm realm2 = context.Realms.Where(i => i.RealmName.Contains("realm1")).FirstOrDefault();
                //AdminUser user = new AdminUser();
                //user.Id = 1;
                //user.Name = "user1";
                //user.Realm = r;
                //context.Add<AdminUser>(user);

                //Solution s1 = new Solution();
                //s1.SlnId = 1;
                //s1.SolutionName = "sln1";                

                //RealmSolution realmSolution = new RealmSolution();
                //realmSolution.RlmId = 1;
                //s1.RealmSolutions.Add(realmSolution);

                //context.Add(s1);

                context.SaveChanges(true);




                //AdminUser us = context.AdminUsers.Include(u=>u.Realm).Where(u => u.Id == 1).FirstOrDefault();
                //Realm rl = context.Realms.Where(rlm => rlm.RlmId == 1).FirstOrDefault();

                //Realm r2 = new Realm();
                //r2.RlmId = 2;
                //r2.RealmName = "r1";


                //context.SaveChanges(true);


                //Realm realm = context.Realms.Where(r => r.RlmId == 2).FirstOrDefault();
                //realm.RealmSolutions = new List<RealmSolution>();
                //realm.RealmSolutions.Add(new RealmSolution() { SlnId = 2 });
                //context.Update(realm);
                //context.SaveChanges(true);
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    }
}
