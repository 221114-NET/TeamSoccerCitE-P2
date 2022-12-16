-- Based on the ERD for the Database, we made these SQL scripts
-- TODO Table for Customers
CREATE TABLE Customer(
    CustomerId VARCHAR(36) PRIMARY KEY,
    Email VARCHAR(50) NOT NULL UNIQUE,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(50) NOT NULL,
    ProfilePic VARBINARY(MAX)
);

-- TODO Table for Products
CREATE TABLE Product(
    ProductId VARCHAR(36) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Description VARCHAR(255) NOT NULL,
    Price FLOAT(53) NOT NULL,
    Quantity INT NOT NULL,
    CategoryId INT NOT NULL,
    ImageLocation VARCHAR(50) NULL
);

-- Table for ProductCategories
CREATE TABLE ProductCategory(
    CategoryId INT PRIMARY KEY,
    CategoryName VARCHAR(50) NOT NULL
);

-- TODO Create table for Customer Session
CREATE TABLE CustomerSession (
    SessionId VARCHAR(36) PRIMARY KEY,
    CustomerId VARCHAR(36) NOT NULL UNIQUE,
    TimeOfLastAction DATETIME NOT NULL,
    CartTotal FLOAT(53) NOT NULL
);

-- TODO create table for Cart Items
-- TODO create table for Order Details

-- Foreign key constraint for the Customer Session table (customer id)
ALTER TABLE CustomerSession ADD CONSTRAINT FK_CustomerId
    FOREIGN KEY (CustomerId) REFERENCES Customer (CustomerId) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Foreign key constraint for the Product table (categories)
ALTER TABLE Product ADD CONSTRAINT FK_CategoryId
    FOREIGN KEY (CategoryId) REFERENCES ProductCategory (CategoryId) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- TODO add foreign key constriant(s) to the Cart Items table
-- TODO add foreign key constraint to the Order Details table