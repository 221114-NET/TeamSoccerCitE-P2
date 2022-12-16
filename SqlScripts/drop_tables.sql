-- Drop foreign key constraint on product table
ALTER TABLE Product DROP CONSTRAINT FK_CategoryId;

-- TODO drop foreign key constraint on customer session table
-- TODO drop foreign key constraint(s) on cart item table
-- TODO drop foreign key constraint on order details table

-- Drop customer table
DROP TABLE Customer;

-- Drop product table
DROP TABLE Product;

-- Drop product category table
DROP TABLE ProductCategory;

-- Drop customer session table
DROP TABLE CustomerSession;

-- TODO drop cart item table
-- TODO drop order details tabe