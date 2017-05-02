SELECT
    [dbo].[users].[user_id],
    [dbo].[users_logins].[login_provider],
    [dbo].[users_logins].[provider_key],
    [dbo].[users_logins].[display_name]
FROM [dbo].[users_logins] (NOLOCK)
    INNER JOIN [dbo].[users] (NOLOCK) ON [dbo].[users].[user_id] = [dbo].[users_logins].[user_id]
WHERE
    [dbo].[users].[user_id] = @UserId
ORDER BY
    [dbo].[users_logins].[login_provider] ASC