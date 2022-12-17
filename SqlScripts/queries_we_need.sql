-- This script has a bunch of queries we'll use when communicating to the Database with ADO.NET
-- Inserting Customer... the NULL here means the customer will have no profile picture...
INSERT INTO Customer (Email, Username, Password, ProfilePic) VALUES('UNIQUEEMAIL', 'UNIQUEUSERNAME', 'PASSWORD', NULL);

-- Inserting Product ... NULL here also means product will have no picture...
INSERT INTO Product (Name, Description, Price, Quantity, CategoryId, ProductImage) VALUES('NAME', 'DESC', 101.11, 99, 2, NULL);

SELECT CustomerId, Email, Username, Password FROM Customer; -- WHERE CustomerId = 1;
SELECT ProductId, Name, Description, Price, Quantity, CategoryId FROM Product;
-- If we change categoryId property in the Product Model to categoryName, do this query instead of above
SELECT ProductId, Name, Description, Price, Quantity, Pc.CategoryName FROM Product P LEFT JOIN ProductCategory Pc ON P.CategoryId = Pc.CategoryId;