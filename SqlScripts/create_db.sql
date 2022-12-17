-- Based on the ERD for the Database
-- Table for Customers
CREATE TABLE Customer(
    CustomerId INT IDENTITY(1, 1) PRIMARY KEY,
    Email VARCHAR(50) NOT NULL UNIQUE,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(50) NOT NULL,
    ProfilePic VARBINARY(MAX) NULL
);

-- Table for Products
CREATE TABLE Product(
    ProductId INT IDENTITY(1, 1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Description VARCHAR(255) NOT NULL,
    Price FLOAT(53) NOT NULL,
    Quantity INT NOT NULL,
    CategoryId INT NOT NULL,
    ProductImage VARBINARY(MAX) NULL
);

-- Table for ProductCategories
CREATE TABLE ProductCategory(
    CategoryId INT PRIMARY KEY,
    CategoryName VARCHAR(50) NOT NULL
);

-- Table for Customer Session
CREATE TABLE LoginSession (
    SessionId VARCHAR(36) PRIMARY KEY, -- GUID
    CustomerId INT NOT NULL UNIQUE,
    TimeOfLastAction DATETIME NOT NULL,
);

-- Foreign key constraint for the Login Session table (customer id)
ALTER TABLE LoginSession ADD CONSTRAINT FK_CustomerId
    FOREIGN KEY (CustomerId) REFERENCES Customer (CustomerId) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Foreign key constraint for the Product table (categories)
ALTER TABLE Product ADD CONSTRAINT FK_CategoryId
    FOREIGN KEY (CategoryId) REFERENCES ProductCategory (CategoryId) ON DELETE NO ACTION ON UPDATE NO ACTION;

-------------------POPULATING ProductCategory----------------------------------
INSERT INTO ProductCategory VALUES (1, 'Shoes'), (2, 'Balls'), (3, 'Jerseys');