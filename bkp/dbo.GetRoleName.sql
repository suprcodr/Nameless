CREATE PROCEDURE [dbo].[GetRoleName] (
    @role_id    UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [name]
    FROM
        [dbo].[roles] (NOLOCK)
    WHERE
        [role_id] = @role_id;

    RETURN 0
END
GO