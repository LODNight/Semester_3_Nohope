
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Providence;
using Providence.Converters;
using Providence.Models;
using Providence.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(x =>
//{
//    x.TokenValidationParameters = new TokenValidationParameters
//    {

//    }
//});

builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.Converters.Add(new DateConverter());
});


var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<DatabaseContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectionString));

builder.Services.AddScoped<AccountService, AccountServiceImpl>();
builder.Services.AddScoped<BlogService, BlogServiceImpl>();
builder.Services.AddScoped<CouponsService, CouponsServiceImpl>();
builder.Services.AddScoped<CategoryService, CategoryServiceImpl>();
builder.Services.AddScoped<ProductService, ProductServiceImpl>();
builder.Services.AddScoped<OrderService, OrderServiceImpl>();
builder.Services.AddScoped<OrderDetailService, OrderDetailServiceImpl>();
builder.Services.AddScoped<WishlistService, WishlistServiceImpl>();
builder.Services.AddScoped<CartService, CartServiceImpl>();
builder.Services.AddScoped<AddressService, AddressServiceImpl>();
builder.Services.AddScoped<ManufacturerService, ManufacturerServiceImpl>();

var app = builder.Build();


app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );

app.UseStaticFiles();

app.MapControllers();

app.Run();
