CREATE PROCEDURE [dbo].[GetRoleNormalizedName] (
    @role_id    UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [normalized_name]
    FROM
        [dbo].[roles] (NOLOCK)
    WHERE
        [role_id] = @role_id;

    RETURN 0
END
GO