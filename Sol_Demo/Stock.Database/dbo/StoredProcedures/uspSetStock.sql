CREATE PROCEDURE [dbo].[uspSetStock]
	
	@Command  Varchar(100)=NULL,

	@StockIdentity uniqueidentifier=NULL,
	@ProductName Varchar(100)=NULL,

	@Quantity int

AS

	DECLARE @ErrorMessage Varchar(MAX)=null;

	DECLARE @AvailableQuantity int

	IF @Command='Stock-Update'
		BEGIN

			BEGIN TRY

				BEGIN TRANSACTION

				
					
					SELECT 
						@AvailableQuantity=SD.Quantity
					FROM 
						StockDetails As SD WITH(NOLOCK)
					WHERE 
						SD.ProductName=@ProductName

					IF @Quantity<=@AvailableQuantity
						BEGIN
							SELECT 'Quantity available' As 'Message';

							SET @Quantity=(@AvailableQuantity - @Quantity)

							UPDATE StockDetails
								SET 
									Quantity=@Quantity
								OUTPUT
									inserted.StockIdentity,
									inserted.ProductName
								WHERE
									ProductName=@ProductName
								

						END
					ELSE
						BEGIN
							SELECT 'Quantity insufficient' As 'Message';
						END

					
				
				COMMIT TRANSACTION

			END TRY

			BEGIN CATCH 
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,19,1)
				ROLLBACK TRANSACTION
			END CATCH

		END
	IF @Command='Stock-RollBack'
		BEGIN

			BEGIN TRY

				BEGIN TRANSACTION

					
					SELECT 
						@AvailableQuantity=SD.Quantity
					FROM 
						StockDetails As SD WITH(NOLOCK)
					WHERE 
						SD.ProductName=@ProductName

					SET @Quantity=@AvailableQuantity + @Quantity

					UPDATE StockDetails
						SET 
							Quantity=@Quantity
						WHERE
							ProductName=@ProductName
					
				
				COMMIT TRANSACTION

			END TRY

			BEGIN CATCH 
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,19,1)
				ROLLBACK TRANSACTION
			END CATCH

		END

GO 