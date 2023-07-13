USE master
GO

DROP DATABASE IF EXISTS Semester_3
CREATE DATABASE Semester_3
GO

USE Semester_3
GO
CREATE TABLE Roles (
	role_id INT PRIMARY KEY IDENTITY,
	role_name VARCHAR(50),
)
GO

CREATE TABLE Account (
  account_id INT PRIMARY KEY IDENTITY,
  firstname NVARCHAR(100),
  lastname NVARCHAR(100),
  email VARCHAR(200) ,
  password VARCHAR(100) ,
  phone VARCHAR(20),
  gender VARCHAR(5),
  address VARCHAR(250),
  avatar VARCHAR(500),
  role_id INT,
  status BIT DEFAULT 1,
  security_code VARCHAR(4),
  created_at DATETIME  DEFAULT GETDATE(),
  updated_at DATETIME DEFAULT NULL,
  FOREIGN KEY (role_id) REFERENCES Roles(role_id)
);
GO

-- =============================
-- =============================
-- Category
-- 1 -> COURSE / PRODUCT
-- 2 -> 
CREATE TABLE Category (
  category_id INT PRIMARY KEY IDENTITY,
  category_name VARCHAR(100) ,
  parent_id INT,
  created_at DATETIME  DEFAULT GETDATE(),
  updated_at DATETIME  DEFAULT NULL,
  FOREIGN KEY (parent_id) REFERENCES Category(category_id)
);
GO

CREATE TABLE Manufacturer (
	mft_id INT PRIMARY KEY IDENTITY,
	mft_name VARCHAR(250), 
	mft_address VARCHAR(250),
	mft_description VARCHAR(250),
)
GO

CREATE TABLE Product (
	product_id INT PRIMARY KEY IDENTITY,
	product_name VARCHAR(250),
	price DECIMAL(18, 2) ,
	category_id INT ,
	description TEXT,
	quantity INT,
	detail TEXT,
	expire_date VARCHAR(8),
	manufacturer_id INT,
	hide BIT ,
	created_at DATETIME  DEFAULT GETDATE(),
	updated_at DATETIME  DEFAULT NULL,
	FOREIGN KEY (category_id) REFERENCES Category(category_id),
	FOREIGN KEY (manufacturer_id) REFERENCES Manufacturer(mft_id)
);
GO


-- 1 Sản phầm có nhìu ảnh
CREATE TABLE ProductImages (
  image_id INT PRIMARY KEY IDENTITY,
  image_url VARCHAR(250),
  product_id INT,
  FOREIGN KEY (product_id) REFERENCES Product(product_id)
);
GO

-- =============================
-- =============================
-- Payment

CREATE TABLE PaymentMethod (
	payment_id INT PRIMARY KEY IDENTITY,
	payment_name VARCHAR(100) ,
	created_at DATETIME  DEFAULT GETDATE(),
	updated_at DATETIME  DEFAULT NULL
);
GO

-- =============================
-- =============================
-- Wish List 

CREATE TABLE Wishlist (
	wishlist_id INT PRIMARY KEY IDENTITY,
	account_id INT,
	product_id INT,
	created_at DATETIME DEFAULT GETDATE(),
	updated_at DATETIME DEFAULT NULL,
	FOREIGN KEY (account_id) REFERENCES Account(account_id),
	FOREIGN KEY (product_id) REFERENCES Product(product_id),
)
GO


-- =============================
-- =============================
-- Coupons 

CREATE TABLE CouponsType(
	coupons_type_id INT PRIMARY KEY IDENTITY,
	name_type VARCHAR(200) , -- Có 2 dạng nếu là [fixed] thì là -100, nếu là [Persent] thì -100%
	isUsed BIT DEFAULT 1,
	created_at DATETIME  DEFAULT GETDATE(),
	updated_at DATETIME  DEFAULT NULL
);
GO

CREATE TABLE Coupons (
	coupon_id INT PRIMARY KEY IDENTITY,
	coupons_type_id INT ,
	coupon_name VARCHAR(200) ,
	discount INT ,
	[description] TEXT ,
 	created_at DATETIME  DEFAULT GETDATE(),
	updated_at DATETIME  DEFAULT NULL,
	FOREIGN KEY (coupons_type_id) REFERENCES CouponsType(coupons_type_id)
);
GO

CREATE TABLE AccountCoupon(
	coupon_id INT ,
	account_id INT ,
	PRIMARY KEY (coupon_id, account_id),
	FOREIGN KEY (coupon_id) REFERENCES Coupons(coupon_id),
	FOREIGN KEY (account_id) REFERENCES Account(account_id)
);
GO

-- =============================
-- =============================
-- ORDER

CREATE TABLE OrderStatus (
  order_status_id INT PRIMARY KEY IDENTITY,
  status_name VARCHAR(100) ,
  status_description VARCHAR(100) ,
  created_at DATETIME  DEFAULT GETDATE(),
  updated_at DATETIME  DEFAULT NULL,
);
GO


CREATE TABLE Orders (
  order_id INT PRIMARY KEY IDENTITY,
  account_id INT ,
  total_price DECIMAL(18,2),
  order_status_id INT ,
  coupon_id INT,
  created_at DATETIME  DEFAULT GETDATE(),
  updated_at DATETIME  DEFAULT NULL,
  FOREIGN KEY (account_id) REFERENCES Account(account_id),
  FOREIGN KEY (order_status_id) REFERENCES OrderStatus(order_status_id),
  FOREIGN KEY (coupon_id) REFERENCES Coupons(coupon_id),
);
GO

--=========
CREATE TABLE OrderDetail (
	order_detail_id INT PRIMARY KEY IDENTITY,
	order_id INT ,
	product_id INT ,
	quantity INT ,
	price DECIMAL(18,2) ,
	created_at DATETIME  DEFAULT GETDATE(),
	updated_at DATETIME  DEFAULT NULL,
	FOREIGN KEY (order_id) REFERENCES Orders(order_id),
	FOREIGN KEY (product_id) REFERENCES Product(product_id),
);
GO

-- =============================
-- =============================
-- Cart

CREATE TABLE Cart (
	cart_id INT PRIMARY KEY IDENTITY,
	account_id INT ,
	created_at DATETIME  DEFAULT GETDATE(),
	updated_at DATETIME  DEFAULT NULL,
	FOREIGN KEY (account_id) REFERENCES Account(account_id)
);
GO

CREATE TABLE CartDetail (
	cart_detail_id INT PRIMARY KEY IDENTITY,
	cart_id INT ,
	product_id INT,	
	quantity INT ,
	price DECIMAL(18,2) ,
	created_at DATETIME  DEFAULT GETDATE(),
	updated_at DATETIME  DEFAULT NULL,
	FOREIGN KEY (cart_id) REFERENCES Cart(cart_id),
	FOREIGN KEY (product_id) REFERENCES Product(product_id),
);
GO

CREATE TABLE InvoiceStatus (
	invoice_status_id INT PRIMARY KEY IDENTITY,
	status_name VARCHAR(50),
	status_description VARCHAR(250),
);
GO

CREATE TABLE Invoice (
	cart_id INT PRIMARY KEY IDENTITY,
	account_id INT ,
	payment_id INT,
	invoice_status_id INT,
	created_at DATETIME  DEFAULT GETDATE(),
	updated_at DATETIME  DEFAULT NULL,
	FOREIGN KEY (account_id) REFERENCES Account(account_id),
	FOREIGN KEY (payment_id) REFERENCES PaymentMethod(payment_id),
	FOREIGN KEY (invoice_status_id) REFERENCES InvoiceStatus(invoice_status_id)
);
GO

-- ============================
-- ============================
-- Blog
CREATE TABLE Blogs (
	blog_id INT PRIMARY KEY IDENTITY,
	blog_name VARCHAR(250),
	blog_image VARCHAR(250),
	short_description VARCHAR(250),
	long_description TEXT,
	hide BIT, 
	created_at DATETIME  DEFAULT GETDATE(),
	updated_at DATETIME  DEFAULT NULL,
);
GO

-- ============================
-- ============================
-- REVIEW

CREATE TABLE Review (
	review_product_id INT PRIMARY KEY IDENTITY,
	account_id INT ,
	product_id INT,
	blog_id INT,
	content TEXT,
	rating TINYINT ,
	created_at DATETIME  DEFAULT GETDATE(),
	updated_at DATETIME  DEFAULT NULL,
	FOREIGN KEY (account_id) REFERENCES Account(account_id),
	FOREIGN KEY (product_id) REFERENCES Product(product_id),
	FOREIGN KEY (blog_id) REFERENCES Blogs(blog_id),
);
GO


