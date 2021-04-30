CREATE TABLE [dbo].[OrderDispatchDetails]
(
	[OrderDispatchId] NUMERIC IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ShippingIdentity] UNIQUEIDENTIFIER NULL, 
    [OrderIdentity] UNIQUEIDENTIFIER NULL, 
    [ShipDate] DATE NULL, 
    [IsCancel] BIT NULL,

)
