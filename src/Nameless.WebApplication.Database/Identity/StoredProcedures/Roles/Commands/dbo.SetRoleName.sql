CREATE PROCEDURE [dbo].[SetRoleName] (
    @role_id    UNIQUEIDENTIFIER,
    @name       NVARCHAR(256)
) AS
BEGIN
    UPDATE [dbo].[roles] SET
        [name] = @name
    WHERE
        [role_id] = @role_id;

    RETURN 0
END
GO