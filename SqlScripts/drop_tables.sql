-- Drop foreign key constraint on product table
ALTER TABLE Product DROP CONSTRAINT FK_CategoryId;

-- Drop foreign key constraint on login session table
ALTER TABLE LoginSession DROP CONSTRAINT FK_CustomerId;

-- Drop customer table
DROP TABLE Customer;

-- Drop product table
DROP TABLE Product;

-- Drop product category table
DROP TABLE ProductCategory;

-- Drop customer session table
DROP TABLE LoginSession;