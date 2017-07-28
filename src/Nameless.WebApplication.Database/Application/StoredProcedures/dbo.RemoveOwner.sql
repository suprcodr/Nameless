CREATE PROCEDURE [dbo].[RemoveOwner]
    @owner_id   UNIQUEIDENTIFIER
AS
BEGIN
    DELETE
    FROM [dbo].[owners]
    WHERE
        [owner_id] = @owner_id;

    RETURN 0
END
GO