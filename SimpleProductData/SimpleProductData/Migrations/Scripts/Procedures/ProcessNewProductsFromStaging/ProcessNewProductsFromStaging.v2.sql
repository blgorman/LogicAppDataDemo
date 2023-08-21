CREATE OR ALTER PROCEDURE dbo.ProcessNewProductsFromStaging
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @NewProductDataCursor CURSOR;
	DECLARE @JSONProductData NVARCHAR(max)
	DECLARE @rowID int
	DECLARE @i int
	DECLARE @length int

    SET @NewProductDataCursor = CURSOR FOR SELECT Id FROM NewProducts WHERE IsProcessed = 0     

    OPEN @NewProductDataCursor 
    FETCH NEXT FROM @NewProductDataCursor 
    INTO @rowID

    WHILE @@FETCH_STATUS = 0
    BEGIN
	  --get the JSON Product Data
	  SET @JSONProductData = (SELECT JSONData FROM NewProducts WHERE Id = @rowID)

	  DECLARE @JsonDataTable table (record nvarchar(MAX))
	  INSERT INTO @JsonDataTable Values (@JSONProductData)

	  INSERT INTO dbo.Products
	  (SKU, [Name], Price, CategoryId)
	  SELECT
		Product.SKU,
		Product.[Name],
		CAST(Product.Price as DECIMAL(18,2)) as Price,
		CAST(Product.CategoryId as int) as CategoryId
	  FROM @JsonDataTable as ProductJson
	  OUTER APPLY (
		SELECT SKU, [Name], Price, CategoryId
		FROM OPENJSON ( ProductJson.record ) 
		WITH (
		  SKU nvarchar(32) '$.SKU',
		  [Name] nvarchar(50) '$.Name',
		  Price nvarchar(MAX) '$.Price',
		  CategoryId nvarchar(MAX) '$.CategoryId'
		)) AS Product

	  IF @@ERROR = 0
	  BEGIN
	    UPDATE NewProducts SET IsProcessed = 1 where Id = @rowID
	  END

      FETCH NEXT FROM @NewProductDataCursor 
      INTO @rowID 
	END

    CLOSE @NewProductDataCursor;
    DEALLOCATE @NewProductDataCursor;
END
GO


