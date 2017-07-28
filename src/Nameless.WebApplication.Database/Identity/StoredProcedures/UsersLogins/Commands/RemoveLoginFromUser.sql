DELETE FROM [dbo].[users_logins]
WHERE
    [dbo].[users_logins].[user_id] = @user_id AND
    [dbo].[users_logins].[login_provider] = @login_provider
