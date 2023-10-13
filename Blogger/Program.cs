using Blogger.Data;
using Blogger.Data.Entities;
using Blogger.Repo;
using Blogger.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<BloggerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BloggerConnectionString")));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<BloggerDbContext>();

// Add Repositories.
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserBlogRepository, UserBlogRepository>();

// Add Services to the container.
//builder.Services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddTransient<IBlogService, BlogService>();
builder.Services.AddTransient<IUserBlogService, UserBlogService>();

var app = builder.Build();

using var serviceScope = app.Services.CreateScope();
{
    var context = serviceScope.ServiceProvider.GetRequiredService<BloggerDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
