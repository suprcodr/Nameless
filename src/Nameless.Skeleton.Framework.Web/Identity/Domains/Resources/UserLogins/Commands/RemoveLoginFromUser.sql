DELETE FROM [dbo].[users_logins]
WHERE
    [dbo].[users_logins].[user_id] = @UserId AND
    [dbo].[users_logins].[login_provider] = @LoginProvider