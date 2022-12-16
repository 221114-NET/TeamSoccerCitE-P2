-- Based on the ERD for the Database, we made these SQL scripts
-- Table for Customers
CREATE TABLE Customer(
    CustomerId VARCHAR(36) PRIMARY KEY,
    Email VARCHAR(50) NOT NULL UNIQUE,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(50) NOT NULL,
    ProfilePic VARBINARY(MAX)
);

-- Table for Products
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

-- TODO create table for Customer Session
-- TODO create table for Cart Items
-- TODO create table for Order Details

-- TODO add foreign key constraint to the Customer Session table
-- TODO add foreign key constraint to the Product table
-- TODO add foreign key constriant(s) to the Cart Items table
-- TODO add foreign key constraint to the Order Details table