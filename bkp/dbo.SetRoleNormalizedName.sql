CREATE PROCEDURE [dbo].[SetRoleNormalizedName] (
    @role_id            UNIQUEIDENTIFIER,
    @normalized_name    NVARCHAR(256)
) AS
BEGIN
    UPDATE [dbo].[roles] SET
        [normalized_name] = @normalized_name
    WHERE
        [role_id] = @role_id;

    RETURN 0
END
GO