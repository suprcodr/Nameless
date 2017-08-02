CREATE PROCEDURE [dbo].[GetUserLogins] (
    @user_id    UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [user_id],
        [login_provider],
        [provider_key],
        [display_name]
    FROM [dbo].[users_logins] (NOLOCK)
    WHERE
        [user_id] = @user_id
    ORDER BY
        [login_provider] ASC;

    RETURN 0
END
GO