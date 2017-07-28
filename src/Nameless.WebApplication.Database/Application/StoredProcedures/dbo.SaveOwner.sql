CREATE PROCEDURE [dbo].[SaveOwner]
    @owner_id   UNIQUEIDENTIFIER,
    @name       NVARCHAR(256)
AS
BEGIN
    IF EXISTS(SELECT 1 FROM [dbo].[owners] WHERE [owner_id] = @owner_id)
    BEGIN
        UPDATE [dbo].[owners] SET
            [name] = @name
        WHERE
            [owner_id] = @owner_id;
    END
    ELSE
    BEGIN
        INSERT INTO [dbo].[owners] (
            [owner_id],
            [name]
        ) VALUES (
            @owner_id,
            @name
        );
    END

    RETURN 0
END
GO