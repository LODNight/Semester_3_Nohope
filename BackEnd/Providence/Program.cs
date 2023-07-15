
using Providence.Converters;
using Providence.Models;
using Providence.Service.Implement;
using Providence.Service.Implement.OutCRUD;
using Providence.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

//
builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.Converters.Add(new DateConverter());
});


builder.Services.AddScoped<DatabaseContext>();


// Add Scoped Service
builder.Services.AddScoped<IServiceCRUD<Blog>, BlogCRUD>();
//Product
builder.Services.AddScoped<IServiceCRUD<Product>, ProductCRUD>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IServiceCRUD<AdministrativeUnit>, AdministrativeUnitCRUD>();
builder.Services.AddScoped<IServiceCRUD<BlogReview>, BlogReviewCRUD>();
builder.Services.AddScoped<IServiceCRUD<CartDetail>, CartDetailCRUD>();
builder.Services.AddScoped<IServiceCRUD<Role>, RoleCRUD>();
builder.Services.AddScoped<IServiceCRUD<Category>, CategoryCRUD>();
builder.Services.AddScoped<IServiceCRUD<Cart>, CartCRUD>();
builder.Services.AddScoped<IServiceCRUD<ProductImage>, ProductImageCRUD>();
builder.Services.AddScoped<IServiceCRUD<Coupon>, CouponCRUD>();
builder.Services.AddScoped<IServiceCRUD<CouponType>, CouponTypeCRUD>();
builder.Services.AddScoped<IServiceCRUD<OrderDetail>, OrderDetailCRUD>();
builder.Services.AddScoped<IServiceCRUD<OrderStatus>, OrderStatusCRUD>();
builder.Services.AddScoped<IServiceCRUD<ProductReview>, ProductReviewCRUD>();
builder.Services.AddScoped<IServiceCRUD<District>, DistrictCRUD>();
builder.Services.AddScoped<IServiceCRUD<Province>, ProvinceCRUD>();
builder.Services.AddScoped<IServiceCRUD<Wishlist>, WishlistCRUD>();
builder.Services.AddScoped<IServiceCRUD<PaymentMethod>, PaymentMethodCRUD>();
builder.Services.AddScoped<IServiceCRUD<Account>, AccountCRUD>();
builder.Services.AddScoped<IServiceCRUD<Order>, OrderCRUD>();
builder.Services.AddScoped<IServiceCRUD<Manufacturer>, ManufacturerCRUD>();
builder.Services.AddScoped<IServiceCRUD<Address>, AddressCRUD>();
builder.Services.AddScoped<IServiceCRUD<Ward>, WardCRUD>();
builder.Services.AddScoped<IServiceCRUD<AccountCoupon>, AccountCouponCRUD>();



var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );

app.UseStaticFiles();

app.MapControllers();

app.Run();
