CREATE PROCEDURE [dbo].[UpdateRole] (
    @role_id            UNIQUEIDENTIFIER,
    @name               NVARCHAR(256),
    @normalized_name    NVARCHAR(256)
) AS
BEGIN
    UPDATE [dbo].[roles] SET
        [name] = @name,
        [normalized_name] = @normalized_name
    WHERE
        [role_id] = @role_id;

    RETURN 0
END
GO