
using Microsoft.EntityFrameworkCore;
using Providence.Converters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.Converters.Add(new DateConverter());
});

//var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

//builder.Services.AddDbContext<DatabaseContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectionString));

//builder.Services.AddScoped<CustomerService, CustomerServiceImpl>();
//builder.Services.AddScoped<OrderService, OrderServiceImpl>();

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
