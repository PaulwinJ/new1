USE crud1;
GO
ALTER TABLE tbl_ProductMaster
ADD Category NVARCHAR(50),
    ProductType NVARCHAR(50),
    ManufactureDate DATE,
    InStock BIT;


select * from tbl_ProductMaster


USE crud1;
GO
CREATE PROCEDURE sp_GetAllProducts
AS
BEGIN
    SELECT ProductID, ProductName, Price, Qty, Remarks, Category, ProductType, ManufactureDate, InStock
    FROM dbo.tbl_ProductMaster
END
GO



USE crud1;
GO
CREATE PROCEDURE sp_InsertProducts
    @ProductName NVARCHAR(50),
    @Price DECIMAL,
    @Qty INT,
    @Remarks NVARCHAR(255),
    @Category NVARCHAR(50),
    @ProductType NVARCHAR(50),
    @ManufactureDate DATE,
    @InStock BIT
AS
BEGIN
    INSERT INTO dbo.tbl_ProductMaster (ProductName, Price, Qty, Remarks, Category, ProductType, ManufactureDate, InStock)
    VALUES (@ProductName, @Price, @Qty, @Remarks, @Category, @ProductType, @ManufactureDate, @InStock)
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[sp_GetProductByID]
(
	@ProductID int
)
as
begin

	select ProductID,ProductName,Price,qty,Remarks from dbo.tbl_ProductMaster with(nolock)
	where ProductID = @ProductID

end
GO










SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE crud1;
GO
CREATE PROCEDURE sp_UpdateProducts
    @ProductID INT,
    @ProductName NVARCHAR(50),
    @Price DECIMAL,
    @Qty INT,
    @Remarks NVARCHAR(255),
    @Category NVARCHAR(50),
    @ProductType NVARCHAR(50),
    @ManufactureDate DATE,
    @InStock BIT
AS
BEGIN
    UPDATE dbo.tbl_ProductMaster
    SET ProductName = @ProductName,
        Price = @Price,
        Qty = @Qty,
        Remarks = @Remarks,
        Category = @Category,
        ProductType = @ProductType,
        ManufactureDate = @ManufactureDate,
        InStock = @InStock
    WHERE ProductID = @ProductID
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_DeleteProduct]
(
@ProductID int
)
as
begin

	delete from dbo.tbl_ProductMaster where ProductID = @ProductID

end
GO










CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Gender NVARCHAR(10),
    Country NVARCHAR(50)
);
GO


USE crud1;
GO

ALTER TABLE Users
DROP COLUMN Role;
GO




USE crud1;
GO

CREATE PROCEDURE sp_RegisterUser
    @Username NVARCHAR(50),
    @Password NVARCHAR(255),
    @Email NVARCHAR(100),
    @Gender NVARCHAR(10),
    @Country NVARCHAR(50),
AS
BEGIN
    INSERT INTO Users (Username, Password, Email, Gender, Country)
    VALUES (@Username, @Password, @Email, @Gender, @Country );
END;
GO

USE crud1;
GO

CREATE PROCEDURE sp_AuthenticateUser
    @Username NVARCHAR(50),
    @Password NVARCHAR(255)
AS
BEGIN
    SELECT COUNT(*)
    FROM Users
    WHERE Username = @Username AND Password = @Password;
END;
GO
USE crud1;
GO




select * from Users


