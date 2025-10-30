using Macone.Data;
using Macone.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add EF Core with SQL Server
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("MaconeConnection"))
);

// Add repository and service dependencies
// ------------------- Admin -------------------
builder.Services.AddScoped<Macone.Areas.Admin.Repositories.IProductRepository,
                           Macone.Areas.Admin.Repositories.Impl.ProductRepository>();
builder.Services.AddScoped<Macone.Areas.Admin.Services.IProductService,
                           Macone.Areas.Admin.Services.Impl.ProductService>();
builder.Services.AddScoped<Macone.Areas.Admin.Repositories.ICategoryRepository,
                           Macone.Areas.Admin.Repositories.Impl.CategoryRepository>();
builder.Services.AddScoped<Macone.Areas.Admin.Services.ICategoryService,
                           Macone.Areas.Admin.Services.Impl.CategoryService>();

// ------------------- User -------------------
builder.Services.AddScoped<Macone.Areas.User.Repositories.IProductRepository,
                           Macone.Areas.User.Repositories.Impl.ProductRepository>();
builder.Services.AddScoped<Macone.Areas.User.Services.IProductService,
                           Macone.Areas.User.Services.Impl.ProductService>();
builder.Services.AddScoped<Macone.Areas.User.Repositories.IShopDetailsRepository,
                           Macone.Areas.User.Repositories.Impl.ShopDetailsRepository>();
builder.Services.AddScoped<Macone.Areas.User.Services.IShopDetailsService,
                           Macone.Areas.User.Services.Impl.ShopDetailsService>();

// ------------------- Account -------------------
builder.Services.AddScoped<Macone.Repositories.IUserRepository,
                           Macone.Repositories.Impl.UserRepository>();
builder.Services.AddScoped<Macone.Services.IAccountService,
                           Macone.Services.Impl.AccountService>();



// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Controller
builder.Services.AddControllers();

// Add Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

//app.UseAuthMiddleware();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
