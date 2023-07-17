using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Providence.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountCoupon> AccountCoupons { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AdministrativeUnit> AdministrativeUnits { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<BlogReview> BlogReviews { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartDetail> CartDetails { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<CouponType> CouponTypes { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductReview> ProductReviews { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=NHATTIN\\SQLEXPRESS;Database=Semester_3;user id=sa;password=123123;trusted_connection=true;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__46A222CD70EDB8F0");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.Avatar)
                .HasMaxLength(500)
                .HasColumnName("avatar");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Gender)
                .HasMaxLength(5)
                .HasColumnName("gender");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.SecurityCode)
                .HasMaxLength(4)
                .HasColumnName("security_code");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Address).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__Account__address__3B75D760");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Account__role_id__3A81B327");
        });

        modelBuilder.Entity<AccountCoupon>(entity =>
        {
            entity.HasKey(e => new { e.CouponId, e.AccountId }).HasName("PK__AccountC__9CA541A51E93ECDC");

            entity.ToTable("AccountCoupon");

            entity.Property(e => e.CouponId).HasColumnName("coupon_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.IsUsed).HasColumnName("is_used");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountCoupons)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountCo__accou__5EBF139D");

            entity.HasOne(d => d.Coupon).WithMany(p => p.AccountCoupons)
                .HasForeignKey(d => d.CouponId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountCo__coupo__5DCAEF64");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Address__CAA247C8FE4034A3");

            entity.ToTable("Address");

            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(20)
                .HasColumnName("district_code");
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(20)
                .HasColumnName("province_code");
            entity.Property(e => e.RoadName)
                .HasMaxLength(255)
                .HasColumnName("road_name");
            entity.Property(e => e.WardCode)
                .HasMaxLength(20)
                .HasColumnName("ward_code");

            entity.HasOne(d => d.DistrictCodeNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.DistrictCode)
                .HasConstraintName("FK__Address__distric__31EC6D26");

            entity.HasOne(d => d.ProvinceCodeNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.ProvinceCode)
                .HasConstraintName("FK__Address__provinc__32E0915F");

            entity.HasOne(d => d.WardCodeNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.WardCode)
                .HasConstraintName("FK__Address__ward_co__30F848ED");
        });

        modelBuilder.Entity<AdministrativeUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("administrative_units_pkey");

            entity.ToTable("AdministrativeUnit");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CodeName)
                .HasMaxLength(255)
                .HasColumnName("code_name");
            entity.Property(e => e.CodeNameEn)
                .HasMaxLength(255)
                .HasColumnName("code_name_en");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.FullNameEn)
                .HasMaxLength(255)
                .HasColumnName("full_name_en");
            entity.Property(e => e.ShortName)
                .HasMaxLength(255)
                .HasColumnName("short_name");
            entity.Property(e => e.ShortNameEn)
                .HasMaxLength(255)
                .HasColumnName("short_name_en");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blog__2975AA282B269763");

            entity.ToTable("Blog");

            entity.Property(e => e.BlogId).HasColumnName("blog_id");
            entity.Property(e => e.BlogImage)
                .HasMaxLength(250)
                .HasColumnName("blog_image");
            entity.Property(e => e.BlogName)
                .HasMaxLength(250)
                .HasColumnName("blog_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Hide)
                .HasDefaultValueSql("((0))")
                .HasColumnName("hide");
            entity.Property(e => e.LongDescription)
                .HasColumnType("text")
                .HasColumnName("long_description");
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(250)
                .HasColumnName("short_description");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<BlogReview>(entity =>
        {
            entity.HasKey(e => e.BlogReviewId).HasName("PK__BlogRevi__2539138E328633A7");

            entity.ToTable("BlogReview");

            entity.Property(e => e.BlogReviewId).HasColumnName("blog_review_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.BlogId).HasColumnName("blog_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Account).WithMany(p => p.BlogReviews)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__BlogRevie__accou__09A971A2");

            entity.HasOne(d => d.Blog).WithMany(p => p.BlogReviews)
                .HasForeignKey(d => d.BlogId)
                .HasConstraintName("FK__BlogRevie__blog___0A9D95DB");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__2EF52A27F4CA5F7E");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Account).WithMany(p => p.Carts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Cart__account_id__74AE54BC");
        });

        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity.HasKey(e => e.CartDetailId).HasName("PK__CartDeta__0F08F529583F6ADC");

            entity.ToTable("CartDetail");

            entity.Property(e => e.CartDetailId).HasColumnName("cart_detail_id");
            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__CartDetai__cart___797309D9");

            entity.HasOne(d => d.Product).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__CartDetai__produ__7A672E12");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__D54EE9B431B9467A");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.CouponId).HasName("PK__Coupon__58CF6389963F7D97");

            entity.ToTable("Coupon");

            entity.Property(e => e.CouponId).HasColumnName("coupon_id");
            entity.Property(e => e.CouponName)
                .HasMaxLength(200)
                .HasColumnName("coupon_name");
            entity.Property(e => e.CouponTypeId).HasColumnName("coupon_type_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.ExpiredAt)
                .HasColumnType("datetime")
                .HasColumnName("expired_at");

            entity.HasOne(d => d.CouponType).WithMany(p => p.Coupons)
                .HasForeignKey(d => d.CouponTypeId)
                .HasConstraintName("FK__Coupon__coupon_t__5AEE82B9");
        });

        modelBuilder.Entity<CouponType>(entity =>
        {
            entity.HasKey(e => e.CouponTypeId).HasName("PK__CouponTy__AD2AFC0A65C7849E");

            entity.ToTable("CouponType");

            entity.Property(e => e.CouponTypeId).HasColumnName("coupon_type_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.TypeName)
                .HasMaxLength(200)
                .HasColumnName("type_name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("District_pkey");

            entity.ToTable("District");

            entity.HasIndex(e => e.ProvinceCode, "idx_district_province");

            entity.HasIndex(e => e.AdministrativeUnitId, "idx_district_unit");

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.AdministrativeUnitId).HasColumnName("administrative_unit_id");
            entity.Property(e => e.CodeName)
                .HasMaxLength(255)
                .HasColumnName("code_name");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.FullNameEn)
                .HasMaxLength(255)
                .HasColumnName("full_name_en");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NameEn)
                .HasMaxLength(255)
                .HasColumnName("name_en");
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(20)
                .HasColumnName("province_code");

            entity.HasOne(d => d.AdministrativeUnit).WithMany(p => p.Districts)
                .HasForeignKey(d => d.AdministrativeUnitId)
                .HasConstraintName("district_administrative_unit_id_fkey");

            entity.HasOne(d => d.ProvinceCodeNavigation).WithMany(p => p.Districts)
                .HasForeignKey(d => d.ProvinceCode)
                .HasConstraintName("district_province_code_fkey");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.MftId).HasName("PK__Manufact__B4D5DD98277A6A93");

            entity.ToTable("Manufacturer");

            entity.Property(e => e.MftId).HasColumnName("mft_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.MftDescription)
                .HasMaxLength(250)
                .HasColumnName("mft_description");
            entity.Property(e => e.MftName)
                .HasMaxLength(250)
                .HasColumnName("mft_name");

            entity.HasOne(d => d.Address).WithMany(p => p.Manufacturers)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__Manufactu__addre__403A8C7D");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__4659622936966089");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CouponId).HasColumnName("coupon_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderStatusId).HasColumnName("order_status_id");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total_price");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Order__account_i__6754599E");

            entity.HasOne(d => d.Coupon).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CouponId)
                .HasConstraintName("FK__Order__coupon_id__693CA210");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatusId)
                .HasConstraintName("FK__Order__order_sta__68487DD7");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Order__payment_i__6A30C649");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__3C5A4080F11339B7");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__order__6EF57B66");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderDeta__produ__6FE99F9F");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId).HasName("PK__OrderSta__A499CF238052963F");

            entity.ToTable("OrderStatus");

            entity.Property(e => e.OrderStatusId).HasColumnName("order_status_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.StatusDescription)
                .HasMaxLength(100)
                .HasColumnName("status_description");
            entity.Property(e => e.StatusName)
                .HasMaxLength(100)
                .HasColumnName("status_name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__PaymentM__ED1FC9EAB19FE643");

            entity.ToTable("PaymentMethod");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PaymentName)
                .HasMaxLength(100)
                .HasColumnName("payment_name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__47027DF5ED37E601");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Detail)
                .HasColumnType("text")
                .HasColumnName("detail");
            entity.Property(e => e.ExpireDate)
                .HasMaxLength(8)
                .HasColumnName("expire_date");
            entity.Property(e => e.Hide).HasColumnName("hide");
            entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(250)
                .HasColumnName("product_name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Product__categor__44FF419A");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufacturerId)
                .HasConstraintName("FK__Product__manufac__45F365D3");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__ProductI__DC9AC9552B1F7BA5");

            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(250)
                .HasColumnName("image_url");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductIm__produ__48CFD27E");
        });

        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.HasKey(e => e.ProductReviewId).HasName("PK__ProductR__8440EB03FAAA5B0E");

            entity.ToTable("ProductReview");

            entity.Property(e => e.ProductReviewId).HasColumnName("product_review_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Account).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__ProductRe__accou__03F0984C");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductRe__produ__04E4BC85");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("Province_pkey");

            entity.ToTable("Province");

            entity.HasIndex(e => e.AdministrativeUnitId, "idx_Province_unit");

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.AdministrativeRegionId).HasColumnName("administrative_region_id");
            entity.Property(e => e.AdministrativeUnitId).HasColumnName("administrative_unit_id");
            entity.Property(e => e.CodeName)
                .HasMaxLength(255)
                .HasColumnName("code_name");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.FullNameEn)
                .HasMaxLength(255)
                .HasColumnName("full_name_en");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NameEn)
                .HasMaxLength(255)
                .HasColumnName("name_en");

            entity.HasOne(d => d.AdministrativeUnit).WithMany(p => p.Provinces)
                .HasForeignKey(d => d.AdministrativeUnitId)
                .HasConstraintName("province_administrative_unit_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__760965CC65541242");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("ward_pkey");

            entity.ToTable("Ward");

            entity.HasIndex(e => e.DistrictCode, "idx_ward_district");

            entity.HasIndex(e => e.AdministrativeUnitId, "idx_ward_unit");

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.AdministrativeUnitId).HasColumnName("administrative_unit_id");
            entity.Property(e => e.CodeName)
                .HasMaxLength(255)
                .HasColumnName("code_name");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(20)
                .HasColumnName("district_code");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.FullNameEn)
                .HasMaxLength(255)
                .HasColumnName("full_name_en");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NameEn)
                .HasMaxLength(255)
                .HasColumnName("name_en");

            entity.HasOne(d => d.AdministrativeUnit).WithMany(p => p.Wards)
                .HasForeignKey(d => d.AdministrativeUnitId)
                .HasConstraintName("ward_administrative_unit_id_fkey");

            entity.HasOne(d => d.DistrictCodeNavigation).WithMany(p => p.Wards)
                .HasForeignKey(d => d.DistrictCode)
                .HasConstraintName("ward_district_code_fkey");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.WishlistId).HasName("PK__Wishlist__6151514E57C75487");

            entity.ToTable("Wishlist");

            entity.Property(e => e.WishlistId).HasColumnName("wishlist_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Account).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Wishlist__accoun__5165187F");

            entity.HasOne(d => d.Product).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Wishlist__produc__52593CB8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
