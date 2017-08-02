CREATE PROCEDURE [dbo].[GetUserTwoFactorEnabled] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [two_factor_enabled]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO