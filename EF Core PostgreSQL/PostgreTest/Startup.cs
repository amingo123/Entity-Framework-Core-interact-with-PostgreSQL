using Hexagon.UserManagement.EFCorePostgre;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostgreTest
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<UserManagementDbContext>(option => option.UseNpgsql("Server=10.137.114.199;Database=AdminSite;uid=postgres;pwd=Hexagon@2018"));
            services.AddDbContext<UserManagementDbContext>(option => option.UseNpgsql("Server=127.0.0.1;Database=AdminSite;uid=sa;pwd=sa"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }
    }
}