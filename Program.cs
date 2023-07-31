using Microsoft.EntityFrameworkCore;
using EasyProduct.Data;
using EasyProduct.Repository.Interface;
using EasyProduct.Repository;


var builder = WebApplication.CreateBuilder(args);

// Configurar os bancos de dados
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<BancoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

// Adicionar injecao de dependencia
builder.Services.AddScoped<IScreensRepository, ScreensRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IProductsCartRepository, ProductsCartRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();