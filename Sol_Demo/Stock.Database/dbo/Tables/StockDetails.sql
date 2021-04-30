CREATE TABLE [dbo].[StockDetails]
(
	[StockId] NUmeric(18,0) IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[StockIdentity] uniqueIdentifier,
	[ProductName] Varchar(100),
	[Quantity] int,

)
