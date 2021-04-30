CREATE PROCEDURE [dbo].[uspSetOrderDispatch]
	
	@Command Varchar(100)=NULL,

	@ShippingIdentity uniqueIdentifier=null,
	@OrderIdentity uniqueIdentifier=null,

	@ShipDate date=null
AS
	
	DECLARE @ErrorMessage Varchar(MAX)=null;

	IF @Command='Create-Order-Dispatch'
	
		BEGIN

			BEGIN TRY

				BEGIN TRANSACTION

					INSERT INTO OrderDispatchDetails
					(
						ShippingIdentity,
						OrderIdentity,
						ShipDate
					)
					VALUES
					(
						NEWID(),
						@OrderIdentity,
						@ShipDate
					)	
				

				COMMIT TRANSACTION

			END TRY

			BEGIN CATCH 
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,19,1)
				ROLLBACK TRANSACTION
			END CATCH

		END

	IF @Command='Cancel-Order-Dispatch'
	
		BEGIN

			BEGIN TRY

				BEGIN TRANSACTION

					UPDATE OrderDispatchDetails
						SET IsCancel=1
					WHERE OrderIdentity=@OrderIdentity
			

				COMMIT TRANSACTION

			END TRY

			BEGIN CATCH 
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,19,1)
				ROLLBACK TRANSACTION
			END CATCH

		END

GO