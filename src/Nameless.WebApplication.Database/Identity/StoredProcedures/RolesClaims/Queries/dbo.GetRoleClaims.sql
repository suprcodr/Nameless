CREATE PROCEDURE [dbo].[GetRoleClaims] (
    @role_id    UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [role_id],
        [type],
        [value]
    FROM [dbo].[roles_claims] (NOLOCK)
    WHERE
        [role_id] = @role_id
    ORDER BY
        [type] ASC;

    RETURN 0
END
GO