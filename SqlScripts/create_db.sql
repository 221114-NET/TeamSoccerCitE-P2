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
--Fixing Login Session Table. Session ID changed to UNIQUEIDENTIFIER datatype(GUID), with a default value from NEWID(), and added CURRENT_TIMESTAMP default to TimeOfLastAction column --JalalD
ALTER TABLE [dbo].[LoginSession] DROP CONSTRAINT [PK__LoginSes__C9F49290D1E6C123] --removing PK constraint in order to edit column
ALTER TABLE LoginSession ALTER COLUMN SessionId UNIQUEIDENTIFIER NOT NULL --changing datatype to GUID type
ALTER TABLE LoginSession ADD PRIMARY KEY (SessionId) --re adding PK constraint to column
ALTER TABLE LoginSession ADD CONSTRAINT newGuid DEFAULT NEWID() FOR SessionId --this Default automatically adds a unique GUID when a new row is created in this table
ALTER TABLE LoginSession ADD CONSTRAINT loginTime DEFAULT CURRENT_TIMESTAMP FOR TimeOfLastAction --this default automatically timestamps when a new row is created


-- Foreign key constraint for the Login Session table (customer id)
ALTER TABLE LoginSession ADD CONSTRAINT FK_CustomerId
    FOREIGN KEY (CustomerId) REFERENCES Customer (CustomerId) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Foreign key constraint for the Product table (categories)
ALTER TABLE Product ADD CONSTRAINT FK_CategoryId
    FOREIGN KEY (CategoryId) REFERENCES ProductCategory (CategoryId) ON DELETE NO ACTION ON UPDATE NO ACTION;



-------------------Populating ProductCategory Table----------------------------
INSERT INTO ProductCategory VALUES (1, 'Shoes'), (2, 'Balls'), (3, 'Jerseys');

-------------------Populating Product Table------------------------------------
INSERT INTO Product (Name, Description, Price, Quantity, CategoryId, ProductImage) VALUES('Nike Cleats', 'This is a description for shoes.', 111.99, 35, 1, NULL);
INSERT INTO Product (Name, Description, Price, Quantity, CategoryId, ProductImage) VALUES('Soccer Ball', 'This is a description for a ball.', 19.99, 99, 2, NULL);
INSERT INTO Product (Name, Description, Price, Quantity, CategoryId, ProductImage) VALUES('Argentina Jersey', 'This is a description for a jersey.', 34.99, 200, 3, NULL);