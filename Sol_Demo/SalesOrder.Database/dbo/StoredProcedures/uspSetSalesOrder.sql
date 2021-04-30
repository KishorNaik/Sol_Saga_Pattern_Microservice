CREATE PROCEDURE [dbo].[uspSetSalesOrder]
	
	@Command Varchar(100)=NULL,

	@SalesOrderIdentity uniqueidentifier=NULL,
	
	@SalesOrderNumber uniqueidentifier=NULL,
	@OrderDate Date=NULL,
	@ProductName Varchar(100)=NULL,
	@OrderQty Varchar(100)=NULL,
	@UnitPrice money=NULL

AS

	DECLARE @ErrorMessage Varchar(MAX)=null;
	
	IF @Command='Create-Sales-Order'
		BEGIN

			BEGIN TRY

				BEGIN TRANSACTION

					INSERT INTO SalesOrderDetails
					(
						SalesOrderIdentity,
						SalesOrderNumber,
						OrderDate,
						ProductName,
						OrderQty,
						UnitPrice
					)
					OUTPUT
						inserted.SalesOrderIdentity,
						inserted.SalesOrderNumber,
						inserted.OrderDate,
						inserted.ProductName,
						inserted.OrderQty,
						inserted.UnitPrice
					VALUES
					(
						NEWID(),
						NewId(),
						@OrderDate,
						@ProductName,
						@OrderQty,
						@UnitPrice
					)
					


				COMMIT TRANSACTION

			END TRY

			BEGIN CATCH 
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,19,1)
				ROLLBACK TRANSACTION
			END CATCH

		END
	IF @Command='Cancel-Sales-Order'
		BEGIN

			BEGIN TRY

				BEGIN TRANSACTION

					
					UPDATE SalesOrderDetails
						SET IsCancel=1
					WHERE
						SalesOrderIdentity=@SalesOrderIdentity


				COMMIT TRANSACTION

			END TRY

			BEGIN CATCH 
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,19,1)
				ROLLBACK TRANSACTION
			END CATCH

		END

GO
