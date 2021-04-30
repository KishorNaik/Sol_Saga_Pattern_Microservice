CREATE TABLE [dbo].[SalesOrderDetails]
(
	[SalesOrderId] NUMERIC IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [SalesOrderIdentity] UNIQUEIDENTIFIER NOT NULL,
	SalesOrderNumber UNIQUEIDENTIFIER NOT NULL,
	OrderDate Date,
	ProductName Varchar(100),
	OrderQty int, 
    [UnitPrice] MONEY NULL, 
    [IsCancel] BIT NULL,
)
