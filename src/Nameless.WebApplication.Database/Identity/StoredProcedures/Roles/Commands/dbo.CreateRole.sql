CREATE PROCEDURE [dbo].[CreateRole] (
    @role_id    UNIQUEIDENTIFIER,
    @name       NVARCHAR(256),
    @owner_id   UNIQUEIDENTIFIER
) AS
BEGIN
    INSERT INTO [dbo].[roles] (
        [role_id],
        [name],
        [owner_id]
    ) VALUES (
        @role_id,
        @name,
        @owner_id
    );

    RETURN 0
END
GO