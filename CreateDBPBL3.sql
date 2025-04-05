--CREATE TABLE Users (
--    UserID INT IDENTITY(1,1) PRIMARY KEY,
--    Username NVARCHAR(50) UNIQUE NOT NULL,
--    PasswordHash NVARCHAR(255) NOT NULL,
--    Name NVARCHAR(100) NOT NULL,
--    Gender NVARCHAR(10) CHECK (Gender IN ('Male', 'Female', 'Other')),
--    BirthOfDate DATE NULL,
--    Gmail NVARCHAR(100) UNIQUE NOT NULL,
--    PhoneNumber NVARCHAR(15) UNIQUE NOT NULL,
--    Role NVARCHAR(20) CHECK (Role IN ('Admin', 'Seller', 'Buyer')) NOT NULL,
--    Address NVARCHAR(255) NULL,
--    AvatarUrl NVARCHAR(255) NULL,
--    Balance DECIMAL(18,2) DEFAULT 0,
--    TotalPosts INT DEFAULT 0,
--    TotalPurchases INT DEFAULT 0,
--    Rating FLOAT DEFAULT 0,
--    Status NVARCHAR(20) CHECK (Status IN ('Active', 'Banned', 'Inactive')) DEFAULT 'Active',
--    IsVerified BIT DEFAULT 0,
--    LastLoginAt DATETIME NULL,
--    CreatedAt DATETIME DEFAULT GETDATE(),
--    UpdatedAt DATETIME DEFAULT GETDATE()
--);
--GO

---- 3. Tạo bảng Categories
--CREATE TABLE Categories (
--    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
--    CategoryName NVARCHAR(100) UNIQUE NOT NULL,
--    CreatedAt DATETIME DEFAULT GETDATE()
--);

---- 4. Tạo bảng Products
--CREATE TABLE Products (
--    ProductID INT IDENTITY(1,1) PRIMARY KEY,
--    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID) ON DELETE CASCADE ON UPDATE CASCADE,
--    Title NVARCHAR(255) NOT NULL,
--    Description NVARCHAR(MAX) NULL,
--    Price DECIMAL(18,2) NOT NULL,
--    CategoryID INT FOREIGN KEY REFERENCES Categories(CategoryID) ON DELETE SET NULL ON UPDATE CASCADE,
--    Condition NVARCHAR(50) CHECK (Condition IN ('New', 'Like New', 'Used', 'Damaged')) NOT NULL,
--    Images NVARCHAR(MAX) NULL,
--    Location NVARCHAR(255) NOT NULL,
--    Status NVARCHAR(20) CHECK (Status IN ('Pending', 'Approved', 'Sold', 'Rejected')) DEFAULT 'Pending',
--    CreatedAt DATETIME DEFAULT GETDATE(),
--    UpdatedAt DATETIME DEFAULT GETDATE()
--);
--CREATE TABLE Orders (
--    OrderID INT IDENTITY(1,1) PRIMARY KEY,
--    BuyerID INT NOT NULL,
--    VendorID INT NOT NULL,
--    TotalPrice DECIMAL(18,2) NOT NULL,
--    Status NVARCHAR(20) CHECK (Status IN ('Pending', 'Completed', 'Cancelled')) DEFAULT 'Pending',
--    CreatedAt DATETIME DEFAULT GETDATE(),
--    CompletedAt DATETIME NULL,
--    CONSTRAINT FK_Orders_Buyer FOREIGN KEY (BuyerID) REFERENCES Users(UserID) ON DELETE NO ACTION ON UPDATE NO ACTION,
--    CONSTRAINT FK_Orders_Vendor FOREIGN KEY (VendorID) REFERENCES Users(UserID) ON DELETE NO ACTION ON UPDATE NO ACTION
--);
--CREATE TABLE OrderDetails (
--    OrderDetailID INT IDENTITY(1,1) PRIMARY KEY,
--    OrderID INT NOT NULL FOREIGN KEY REFERENCES Orders(OrderID) ON DELETE CASCADE ON UPDATE CASCADE,
--    ProductID INT NOT NULL FOREIGN KEY REFERENCES Products(ProductID) ON DELETE CASCADE ON UPDATE CASCADE,
--    Price DECIMAL(18,2) NOT NULL
--);
--CREATE TABLE Ratings (
--    RatingID INT IDENTITY(1,1) PRIMARY KEY,
--    UserID INT NOT NULL,
--    ProductID INT NOT NULL,
--    RatingValue FLOAT CHECK (RatingValue BETWEEN 1 AND 5) NOT NULL,
--    Comment NVARCHAR(MAX) NULL,
--    CreatedAt DATETIME DEFAULT GETDATE(),
--    CONSTRAINT FK_Ratings_User FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE NO ACTION ON UPDATE NO ACTION,
--    CONSTRAINT FK_Ratings_Product FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE NO ACTION ON UPDATE NO ACTION
--);
--CREATE TABLE Wishlists (
--    WishlistID INT IDENTITY(1,1) PRIMARY KEY,
--    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID) ON DELETE NO ACTION ON UPDATE NO ACTION,
--    ProductID INT NOT NULL FOREIGN KEY REFERENCES Products(ProductID) ON DELETE NO ACTION ON UPDATE NO ACTION,
--    CreatedAt DATETIME DEFAULT GETDATE()
--);
--CREATE TABLE Discounts (
--    DiscountID INT IDENTITY(1,1) PRIMARY KEY,
--    Code NVARCHAR(50) UNIQUE NOT NULL,
--    DiscountPercent FLOAT CHECK (DiscountPercent BETWEEN 0 AND 100) NOT NULL,
--    ExpirationDate DATETIME NOT NULL,
--    IsActive BIT DEFAULT 1
--);
-- Chèn Users
INSERT INTO Users (Username, PasswordHash, Name, Gender, BirthOfDate, Gmail, PhoneNumber, Role, Address, AvatarUrl, Balance, TotalPosts, TotalPurchases, Rating, Status, IsVerified, LastLoginAt, CreatedAt, UpdatedAt)
VALUES 
('admin', 'hashedpassword1', 'Admin User', 'Male', '1990-01-01', 'admin@example.com', '0987654321', 'Admin', 'Hà Nội', NULL, 0, 0, 0, 5.0, 'Active', 1, GETDATE(), GETDATE(), GETDATE()),
('seller1', 'hashedpassword2', 'Nguyễn Văn A', 'Male', '1995-05-10', 'seller1@example.com', '0901234567', 'Seller', 'Hà Nội', 'avatar1.jpg', 1000000, 5, 0, 4.5, 'Active', 1, GETDATE(), GETDATE(), GETDATE()),
('buyer1', 'hashedpassword3', 'Trần Thị B', 'Female', '1998-09-15', 'buyer1@example.com', '0912345678', 'Buyer', 'TP HCM', 'avatar2.jpg', 500000, 0, 2, 4.0, 'Active', 1, GETDATE(), GETDATE(), GETDATE());

-- Chèn Categories
INSERT INTO Categories (CategoryName, CreatedAt)
VALUES ('Điện tử', GETDATE()), ('Quần áo', GETDATE()), ('Nội thất', GETDATE()), ('Sách', GETDATE());

-- Chèn Products
INSERT INTO Products (UserID, Title, Description, Price, CategoryID, Condition, Images, Location, Status, CreatedAt, UpdatedAt)
VALUES 
(2, 'Điện thoại iPhone 12', 'iPhone 12 cũ, còn 99% như mới', 12000000, 1, 'Like New', 'iphone12.jpg', 'Hà Nội', 'Approved', GETDATE(), GETDATE()),
(2, 'Áo khoác nam', 'Áo khoác chống nước, size L', 300000, 2, 'Used', 'aokhoac.jpg', 'TP HCM', 'Approved', GETDATE(), GETDATE());

-- Chèn Orders
INSERT INTO Orders (BuyerID, VendorID, TotalPrice, Status, CreatedAt, CompletedAt)
VALUES (3, 2, 12000000, 'Pending', GETDATE(), NULL), (3, 2, 300000, 'Completed', GETDATE(), GETDATE());

-- Chèn OrderDetails
INSERT INTO OrderDetails (OrderID, ProductID, Price)
VALUES (1, 1, 12000000), (2, 2, 300000);

-- Chèn Ratings
INSERT INTO Ratings (UserID, ProductID, RatingValue, Comment, CreatedAt)
VALUES (3, 1, 5.0, 'Sản phẩm rất tốt, dùng mượt mà!', GETDATE()), (3, 2, 4.0, 'Áo khoác khá ổn, hơi rộng hơn mong đợi.', GETDATE());

-- Chèn Wishlists
INSERT INTO Wishlists (UserID, ProductID, CreatedAt)
VALUES (3, 1, GETDATE()), (3, 2, GETDATE());

-- Chèn Discounts
INSERT INTO Discounts (Code, DiscountPercent, ExpirationDate, IsActive)
VALUES ('WELCOME10', 10, DATEADD(DAY, 30, GETDATE()), 1), ('SUMMER20', 20, DATEADD(DAY, 60, GETDATE()), 1);
