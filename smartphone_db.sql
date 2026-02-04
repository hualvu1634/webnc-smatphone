-- 1. Tạo Database
CREATE DATABASE smartphone_store_db;
GO

USE smartphone_store_db;

GO
CREATE TABLE Categories (
    category_id INT PRIMARY KEY IDENTITY(1,1), 
    category_name NVARCHAR(255) NOT NULL UNIQUE,
    category_date DATETIME DEFAULT GETDATE(),
    
);
INSERT INTO Categories (category_name) VALUES 
(N'Apple'),
(N'Samsung'),
(N'Xiaomi'),
(N'Oppo'),
(N'Vivo');
GO
DELETE FROM Categories;

-- Reset lại cột tự động tăng về 0 (để bản ghi tiếp theo sẽ là 1)
DBCC CHECKIDENT ('Categories', RESEED, 0);
CREATE TABLE Products (
    product_id INT PRIMARY KEY IDENTITY(1,1),
    product_name NVARCHAR(50) NOT NULL UNIQUE,
    descriptions NVARCHAR(255),                
    price DECIMAL(10, 2) NOT NULL,
    image_url VARCHAR(255),                  
    quantity INT DEFAULT 0,
    category_id INT ,
    FOREIGN KEY (category_id) REFERENCES Categories(category_id),
    product_date DATETIME DEFAULT GETDATE()
);
GO

-- 4. Bảng Khách hàng (Customers)
CREATE TABLE Customers (
    customer_id INT PRIMARY KEY IDENTITY(1,1),
    first_name NVARCHAR(255) NOT NULL,
    last_name NVARCHAR(255) NOT NULL,
    email VARCHAR(50) UNIQUE NOT NULL,                  -- Email là duy nhất
    customers_password VARCHAR(50) NOT NULL,            -- Mật khẩu (thường lưu hash)
    customers_address NVARCHAR(255),
    phone_number VARCHAR(20) NOT NULL UNIQUE,
    customer_date DATETIME DEFAULT GETDATE(),
);
GO
GO

CREATE TABLE Carts (
    cart_id INT PRIMARY KEY IDENTITY(1,1),
    customer_id INT UNIQUE, 
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id)
);
GO

CREATE TABLE CartItems (
    cart_item_id INT PRIMARY KEY IDENTITY(1,1),
    cart_id INT,
    product_id INT,
    quantity INT DEFAULT 1, 
    FOREIGN KEY (cart_id) REFERENCES Carts(cart_id),
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
);
GO
-- 5. Bảng Đơn hàng (Orders)
CREATE TABLE Orders (
    order_id INT PRIMARY KEY IDENTITY(1,1),
    customer_id INT,
    address NVARCHAR(255) NOT NULL,
    phone_number VARCHAR(20) NOT NULL,
    order_date DATETIME DEFAULT GETDATE(),     -- Tự động lấy ngày giờ hiện tại
    total_amount DECIMAL(10, 2),
    orders_status NVARCHAR(50) DEFAULT N'Chờ xác nhận', -- Trạng thái mặc định
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id)
);
GO
CREATE TABLE OrderDetails (
    order_detail_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT,
    product_id INT,
    quantity INT NOT NULL,
    price DECIMAL(10, 2) NOT NULL, 
    total_price DECIMAL(10, 2),    
    FOREIGN KEY (order_id) REFERENCES Orders(order_id),
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
);
GO
