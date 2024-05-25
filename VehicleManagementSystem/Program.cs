using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Service.Data;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());



builder.Services.AddDbContext<ApplicationDbContext>(optionsAction: options =>
{
    options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString(name: "Default"));
    // options.UseInMemoryDatabase(databaseName: "InMemoryDb");
});

builder.Services.AddRazorPages();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(setupAction: options =>
    {
        options.User.RequireUniqueEmail = false;

        options.SignIn.RequireConfirmedAccount = false;
    })
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    await app.Services.InitializeAsync();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated(); // This will trigger OnModelCreating
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
