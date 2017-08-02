CREATE PROCEDURE [dbo].[GetUserClaims] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [user_id],
        [type],
        [value]
    FROM [dbo].[users_claims] (NOLOCK)
    WHERE
        [user_id] = @user_id
    ORDER BY
        [type] ASC;

    RETURN 0
END
GO