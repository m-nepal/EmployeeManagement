## This project is  re-created by watching videos by KudVenkat https://www.youtube.com/channel/UCCTVrRB5KpIiK6V2GGVsR1Q for learning purpose.

### Step 1 : Inherit from IdentityDbContext class
public class AppDbContext : IdentityDbContext
{
    // Rest of the code
}

### Step 2 : Add ASP.NET Core Identity Services
In ConfigureServices() method of the Startup class, include the following line of code.

services.AddIdentity[IdentityUser, IdentityRole]()
        .AddEntityFrameworkStores[AppDbContext]();
        
### Step 3 : Add Authentication middleware to the request pipeline
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseStaticFiles();
    app.UseAuthentication();
    app.UseMvc(routes =]
    {
        routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
    });
}
### Step 4 : Add Identity Migration
In Visual Studio, from the Package Manager Console window execute the following command to add a new migration
Add-Migration AddingIdentity
This migration contains code that creates the tables required by the ASP.NET Core Identity system.
Error : The entity type 'IdentityUserLogin[string]' requires a primary key to be defined
If you get this error, the most likely cause is that you are overriding OnModelCreating() method in your application DbContext class but not calling the base IdentityDbContext class OnModelCreating() method. 
Keys of Identity tables are mapped in OnModelCreating method of IdentityDbContext class. So, to fix this error, all you need to do is, call the base class OnModelCreating() method using the base keyword as shown below.
public class AppDbContext : IdentityDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Seed();
    }
}
### Step 5 : Generate ASP.NET Core Identity Tables

Finally execute Update-Database command to apply the identity migration and have the required identity tables created.
