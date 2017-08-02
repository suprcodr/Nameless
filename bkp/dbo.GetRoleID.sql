CREATE PROCEDURE [dbo].[GetRoleID] (
    @normalized_name    NVARCHAR(256),
    @owner_id           UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [role_id]
    FROM
        [dbo].[roles] (NOLOCK)
    WHERE
        [normalized_name] = @normalized_name
    AND [owner_id] = @owner_id;

    RETURN 0
END
GO