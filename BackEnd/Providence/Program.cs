

using Providence.Models;
using Providence.Service.Implement;
using Providence.Service.Implement.OutCRUD;
using Providence.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

//
builder.Services.AddControllers();


builder.Services.AddScoped<DatabaseContext>();


// =============================== Add Scoped Service

// Blog
builder.Services.AddScoped<IServiceCRUD<Blog>, BlogCRUD>(); //- 
builder.Services.AddScoped<IBlogService, BlogService>(); //- 
builder.Services.AddScoped<IServiceCRUD<BlogReview>, BlogReviewCRUD>();
// Product
builder.Services.AddScoped<IServiceCRUD<Product>, ProductCRUD>(); //- 
builder.Services.AddScoped<IProductService, ProductService>(); //- 
builder.Services.AddScoped<IServiceCRUD<ProductImage>, ProductImageCRUD>(); //-
builder.Services.AddScoped<IServiceCRUD<ProductReview>, ProductReviewCRUD>();
// Account
builder.Services.AddScoped<IServiceCRUD<Account>, AccountCRUD>(); //- 
builder.Services.AddScoped<IAccountService, AccountService>(); //- 
builder.Services.AddScoped<IServiceCRUD<AccountCoupon>, AccountCouponCRUD>();
// Coupon
builder.Services.AddScoped<IServiceCRUD<Coupon>, CouponCRUD>(); //-
builder.Services.AddScoped<IServiceCRUD<CouponType>, CouponTypeCRUD>(); //-
// Order
builder.Services.AddScoped<IServiceCRUD<Order>, OrderCRUD>();
builder.Services.AddScoped<IOrderService, OrderService>(); //- 
builder.Services.AddScoped<IServiceCRUD<OrderDetail>, OrderDetailCRUD>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>(); //- 
builder.Services.AddScoped<IServiceCRUD<OrderStatus>, OrderStatusCRUD>();
// Category
builder.Services.AddScoped<IServiceCRUD<Category>, CategoryCRUD>(); //-
// Role
builder.Services.AddScoped<IServiceCRUD<Role>, RoleCRUD>(); //-
// Administrative
builder.Services.AddScoped<IServiceCRUD<AdministrativeUnit>, AdministrativeUnitCRUD>();
// Cart
builder.Services.AddScoped<IServiceCRUD<Cart>, CartCRUD>(); //-
builder.Services.AddScoped<ICartService, CartService>(); //-
builder.Services.AddScoped<IServiceCRUD<CartDetail>, CartDetailCRUD>(); //-
builder.Services.AddScoped<ICartDetailService, CartDetailService>(); //-
// Whistlist
builder.Services.AddScoped<IServiceCRUD<Wishlist>, WishlistCRUD>(); //-
// Payment
builder.Services.AddScoped<IServiceCRUD<PaymentMethod>, PaymentMethodCRUD>(); //-
// Address
builder.Services.AddScoped<IServiceCRUD<Address>, AddressCRUD>(); //-
builder.Services.AddScoped<IServiceCRUD<District>, DistrictCRUD>(); //-
builder.Services.AddScoped<IDistrictService, DistrictService>();

builder.Services.AddScoped<IServiceCRUD<Ward>, WardCRUD>(); //-
builder.Services.AddScoped<IWardService, WardService>();

builder.Services.AddScoped<IServiceCRUD<Province>, ProvinceCRUD>(); //-
builder.Services.AddScoped<IServiceCRUD<Manufacturer>, ManufacturerCRUD>(); //-

// ===========================================

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
