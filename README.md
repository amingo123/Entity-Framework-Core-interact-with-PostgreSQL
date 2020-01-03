
[[_TOC_]]


# About
This Wiki is about how to use Entity Framework Core Code-First approach to interact with PostgreSQL database.
The aim of this is to provide a basic guidance and standard for the Wuhan Team while building the IdentityServer application.

# Brief Description
In the EF Core Code-First approach, as its name suggests when we are starting with the development of a new project, we write C# entity classes first rather than design data storage structure directly in database. All the entity classes can be migrated easily with the database.
The important thing to note is that the manual changes that we make to the database could be lost during migration. So we should make changes to the code only.

# Sampel Project
- ## Prepare Environment 
_Before going further, make sure VS2017 was installed and Postgre Server instance was ready._

Create a .Net Core Class Library Project, I named it - Hexagon.UserManagement.EFCorePostgre.
This project is considered as Data Access Layer which will interact with PostgreSQL database.
 
![image.png](/.attachments/image-d5e6204b-f18a-4945-8645-44d2a409f892.png)

- ## Necessary Components
To interact with Postgres with Entity Framework core, below components are needed. 
Use Nuget to install them into EFCorePostgre project.

|Component|Description|
|--|--|
|Microsoft.EntityFrameworkCore |Core component of Entity Framework Core,provide DbContext and DbSet classes. |
|Microsoft.EntityFrameworkCore.Relational|Common component for relational database providers.|
|Microsoft.EntityFrameworkCore.Tools|Include necessary tools used for generating migration files and update target database.|
|Npgsql.EntityFrameworkCore.PostgreSQL|PostgreSql Database provider for Entity Framework Core https://docs.microsoft.com/zh-cn/ef/core/providers/index

- ## DbContext
DbContext is a concept of Entity Framework which could be simply considered as a database object.
It helps to query data from database and write changes back.
DbContext is usually used with a derived type that contains DbSet&lt;TEntity&gt; properties.
```
public class UserManagementDbContext:DbContext
{
        public UserManagementDbContext(DbContextOptions options) : base(options)
        {
        }
}
```
So far so good, will come back to this class later.

- ## TEntity
TEntity is the data structure we need to define to build our business logic based on requirements.
Like the table structure in database.

- ## DbSet&lt;TEntity&gt;
DbSet can be thought as a table obejct in database along with it data.
DbSet&lt;TEntity&gt; can be used to query and update TEntity instances.

- ## Design Entity
- ### One to Many Relationship
Let's say we have User object and Realm(Organization) object in our system.
A user belongs to one realm only and a realm would have many users.
To represent this relationship, we can define the Class as below.
   
```
    public class AdminUser
    {
        public int Id { get; set; }
        public int? RealmId { get; set; }
        public Realm Realm { get; set; }

        // some other field of user
        public string Name { get; set; }
        public int? Age { get; set; }
        public DateTime? TokenExpireTime { get; set; }
        public string Gender { get; set; }
    }

    public class Realm
    {
        public int RlmId { get; set; }
        public string RealmName { get; set; }
        public ICollection<AdminUser> AdminUser{ get; set; }
    }
```
Now we have data structure defined, we need a way to define field type and relationship between objects.There are three ways to in Entity Framework Core.
1. Notations 
    
```
    public class AdminUser
    {
        [key]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
    }
```
Above shows Id is set as primary key and specify a maximum of 255 characters for the Name.
The notations are very convenient but less powerful compare to the Fluent API.

2. Fluent API
```
public class UserManagementDbContext:DbContext
{
        public UserManagementDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AdminUser>().HasKey(u => u.Id);
            modelBuilder.Entity<AdminUser>().Property(u => u.Name).HasMaxLength(255);
        }
}
```
The OnModelCreating method receives a ModelBuilder object, on which the Fluent API is used.
The most important method defined by the ModelBuilder class is Entity, which provide lots of useful method to define attributes on entity and entity Property.

The flaw of this approach is the OnModelCreating method will become extremely long and unreadable if we put every objects definition here.

[More Flunt API](https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/fluent/types-and-properties)

3. A seperate Configuration Class
    
```
public class UserConfiguration : IEntityTypeConfiguration<AdminUser>
{
        public void Configure(EntityTypeBuilder<AdminUser> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).IsRequired(true).HasMaxLength(255);
        }
}

public class RealmConfiguration : IEntityTypeConfiguration<Realm>
{
        public void Configure(EntityTypeBuilder<Realm> builder)
        {
            builder.HasKey(r => r.RlmId);
            builder.Property(r => r.RealmName).IsRequired(true).HasMaxLength(50);
            builder.HasMany(r => r.AdminUsers);
        }
}

public class UserManagementDbContext:DbContext
{
        public DbSet<AdminUser> AdminUsers { get; set; }
         public DbSet<Realm> Realms { get; set; }

        public UserManagementDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RealmConfiguration());
        }
}
```
Add UserConfiguration and RealmConfiguration class and apply them, short and sweet.

**Note:This would be the suggested pattern when we use Entity Framework Core.**

Last thing is the add the 'table' objects to the 'database' object.
Add `public DbSet<AdminUser> AdminUsers { get; set; }
 public DbSet<Realm> Realms { get; set; }
`  to UserManagementDbContext class.

- ### Many to Many Relationship
Now we have Realm object, a realm can associate with many Solutions and a solution can also associate with many realms.
To represent this relationship, we need three objects - Realm,Solution and middle object RealmSolution.
    
```
public class Realm
{
        public int RlmId { get; set; }

        public string RealmName { get; set; }

        public ICollection<AdminUser> AdminUsers { get; set; }

        public ICollection<RealmSolution> RealmSolutions { get; set; }
}

public class RealmSolution
{
        public int RlmId { get; set; }

        public int SlnId { get; set; }

        public Realm Realm { get; set; }

        public Solution Solution { get; set; }

}

public class Solution
{
        public Solution()
        {
            RealmSolutions = new List<RealmSolution>();
        }

        public int SlnId { get; set; }

        public string SolutionName { get; set; }

        public ICollection<RealmSolution> RealmSolutions { get; set; }
}

public class RealmSolutionConfiguration : IEntityTypeConfiguration<RealmSolution>
    {
        public void Configure(EntityTypeBuilder<RealmSolution> builder)
        {
            builder.HasKey(rs => new { rs.RlmId, rs.SlnId });

            builder.HasOne(pc => pc.Realm)
                .WithMany(p => p.RealmSolutions)
                .HasForeignKey(pc => pc.RlmId);

            builder.HasOne(pc => pc.Solution)
                .WithMany(c => c.RealmSolutions)
                .HasForeignKey(pc => pc.SlnId);
        }
    }

public class SolutionConfiguration : IEntityTypeConfiguration<Solution>
{
        public void Configure(EntityTypeBuilder<Solution> builder)
        {
            builder.HasKey(c => c.SlnId);
            builder.Property(c => c.SolutionName).HasMaxLength(200).IsRequired(true);
        }
}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RealmConfiguration());
            modelBuilder.ApplyConfiguration(new SolutionConfiguration());
            modelBuilder.ApplyConfiguration(new RealmSolutionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
}
```

Entities are ready for promoting to database. Before that, we need to do some extra configuration like connection string, dependency injection.

_This section based on EF Core 2.2. From EF Core 3.0 make an improvement related to represent Many to many relationship, then we can represent it without the join(middle) entity - RealmSolution class in our case._ 

- ## Host
Create a .Net Core Console App - PostgreTest. This is considered as a host or comsumer of the EFCorePostgre library. 

![image.png](/.attachments/image-31bcb6d3-9368-41de-8c07-fa83f645dd93.png)


```
class Program
{
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
}

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
            services.AddDbContext<UserManagementDbContext>(option => option.UseNpgsql(connectionstring));
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }
}
```

Add project reference, select EFCorePostgre. Set this project as the startup project.
Now we can start migrating.

- ## Manage Migration
Migration is a way to keep the database schema in sync with the EF Core entity by preserving data.

- ### Manually
Open 'Package Manager Console' from 'Tools'->'Nuget Package Manager' -> 'Package Manager Console'
![image.png](/.attachments/image-a0d5ba0c-1a7c-442c-aebc-be674f92254f.png)
Select EFCorePostgre as the Defualt project.

PM> _add-migration initial -verbose_
This will generate database update scripts under Migrations folder. 
Everytime you use add-migration command, corresponding scripts will be generated with the version number in this folder and all the database changes can be tracked.

![image.png](/.attachments/image-02151811-06f3-4133-800a-13fa5baf2b70.png)

PM> _update-database -verbose_
This will run update scripts on target database since last update.

All the tables should be created in database now. 
![image.png](/.attachments/image-b1b8afaf-5189-47f7-bdf0-ebd5a95eaabf.png)

**Note:
Before add-migration to promote local entity changes, make sure to get the latest code from GIT.
Database changes should be only made by C# classes,manual changes that we make to the database could be lost during migration. So we should make changes to the code only.**

- ### Programming
Need further investigate.
- ### Pipeline
No sure if it's a good way. Need futher investigate.

- ## PostgerSQL Naming convention
Since we have tables in database, let do a simple query `select * from AdminUsers`.
An error shows "adminusers does not exist". 
![image.png](/.attachments/image-fd7e32f4-f4e7-4b5d-b942-9257d25fc5fc.png)

Notice that we uppercase 'AdminUsers' in the query, but it shows lower-case 'adminusers' in the message.


In Postgres, table name , field name and data are case sensitive, futhermore unquoted names are always changed to lower case.
To solve this, we can add double quotes around the "AdminUsers" - `select * from "AdminUsers"`. 
This way can prevent "Users" changing to "users", but it's inefficiency.
We will set the lower case object name at the first place.
Another thing worth to notice is that the "AdminUser" is in plural form, we just want it to be "AdminUser".
Add some extra code in OnModelCreating to solve this.
```
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RealmConfiguration());
            modelBuilder.ApplyConfiguration(new SolutionConfiguration());
            modelBuilder.ApplyConfiguration(new RealmSolutionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
           
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType == null) continue;

                // lowercase table name & RemovePluralizingTableName
                entityType.Relational().TableName = entityType.ClrType.Name.ToLower();

                //lowercase primary key name
                var key = entityType.FindPrimaryKey();
                if (key != null)
                {
                    key.Relational().Name = key.Relational().Name.ToLower();
                }

                //lowercase column name
                foreach (var property in entityType.GetProperties())
                {
                    property.Relational().ColumnName = property.Relational().ColumnName.ToLower();
                }

                //lowercase ForeignKeys
                foreach (var property in entityType.GetProperties())
                {
                    foreach (var fk in entityType.FindForeignKeys(property))
                    {
                        fk.Relational().Name = fk.Relational().Name.ToLower();
                    }
                }

                //rename index
                foreach (var index in entityType.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name.Replace("[", string.Empty).Replace("]", string.Empty).ToLower();
                }
            }            
}
```

Repeat the migrate,update process query again.

```
select * from adminuser
select * from ADMINUSER
select * from AdminUser
```

![image.png](/.attachments/image-ba7aa542-8fe9-4cbc-a268-3901768b3c87.png)
All queris can run successful, we can now use EFCorePostgre library to put some data and do some query.

- ## Insert Data
Create a Realm named Realm1.
Create a User named User1, add the user to Realm1.
Create a Solution named sln1,associate it with Realm1.
            
```
var optionsBuilder = new DbContextOptionsBuilder<UserManagementDbContext>();
optionsBuilder.UseNpgsql(connectionstring);
using (UserManagementDbContext context = new UserManagementDbContext(optionsBuilder.Options))
{
                Realm r = new Realm();
                r.RlmId = 1;
                r.RealmName = "Realm1";
                context.Add(r);

                AdminUser user = new AdminUser();
                user.Id = 1;
                user.Name = "user1";
                user.Realm = r;
                context.Add(user);

                Solution s1 = new Solution();
                s1.SlnId = 1;
                s1.SolutionName = "sln1";                

                RealmSolution realmSolution = new RealmSolution();
                realmSolution.RlmId = 1;
                s1.RealmSolutions.Add(realmSolution);
                context.Add(s1);

                context.SaveChanges(true);
}
```
**Realm**
![image.png](/.attachments/image-d7f18b62-8273-4542-9281-bdb220139788.png)

**AdminUser**
![image.png](/.attachments/image-d3ba7a1f-a9f1-4c85-a53c-89b76e99405c.png)

**Solution**
![image.png](/.attachments/image-8c534a5e-e0d9-4c69-93b6-2c9935255267.png)

![image.png](/.attachments/image-d583ef87-abbf-4c90-867f-8554e5257613.png)

- ## Query Data
- Exact Match Query
`AdminUser us = context.AdminUsers.Where(u => u.Id == 1).FirstOrDefault();`

![image.png](/.attachments/image-d6a5ff89-5070-441d-9f1c-743e73f9db5f.png)

The values from AdminUser table are returned but Realm Object is null which is expected result for performance purpose - load it when needed.
You can use the Include method to specify related data to be included in query results. 

`AdminUser us = context.AdminUsers.Include(u=>u.Realm).Where(u => u.Id == 1).FirstOrDefault();`

![image.png](/.attachments/image-9ce73b1c-0cca-40d9-a129-eed4fe422804.png)
Realm object has been loaded now.

- Fuzzy Match Query
Data in Postgres are case sensitivity, so below script will return nothing.
```
select * from realm where realmname like '%realm%'
Realm realm1 = context.Realms.Where(i => i.RealmName.Contains("realm1")).FirstOrDefault();
```

The key word **ILIKE** can be used instead of LIKE to make the match case-insensitive and EF Core also provided the ILike Extension Function, it follows the same syntax as Like.
`Realm realm1 =  context.Realms.Where(i => EF.Functions.ILike(i.RealmName, "realm%")).FirstOrDefault();`

# Whole Picture
![image.png](/.attachments/image-689b881d-884a-4e64-af14-9c08166952c2.png)