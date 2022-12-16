-- Drop foreign key constraint on product table
ALTER TABLE Product DROP CONSTRAINT FK_CategoryId;

-- TODO drop foreign key constraint on customer session table
-- TODO drop foreign key constraint(s) on cart item table
-- TODO drop foreign key constraint on order details table

-- Drop customer table
DROP TABLE Customer;

-- Drop product table
DROP TABLE Product;

-- TODO drop product category table
DROP TABLE ProductCategory;

-- TODO drop customer session table
-- TODO drop cart item table
-- TODO drop order details tabe