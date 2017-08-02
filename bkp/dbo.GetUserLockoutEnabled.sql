CREATE PROCEDURE [dbo].[GetUserLockoutEnabled] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [lockout_enabled]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO