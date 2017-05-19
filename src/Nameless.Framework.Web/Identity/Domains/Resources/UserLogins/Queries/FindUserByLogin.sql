SELECT TOP 1
    [dbo].[users].[user_id],
    [dbo].[users].[concurrency_stamp],
    [dbo].[users].[user_name],
    [dbo].[users].[normalized_user_name],
    [dbo].[users].[full_name],
    [dbo].[users].[access_failed_count],
    [dbo].[users].[email],
    [dbo].[users].[email_confirmed],
    [dbo].[users].[normalized_email],
    [dbo].[users].[lockout_enabled],
    [dbo].[users].[lockout_end_date_utc],
    [dbo].[users].[password_hash],
    [dbo].[users].[phone_number],
    [dbo].[users].[phone_number_confirmed],
    [dbo].[users].[two_factor_enabled],
    [dbo].[users].[security_stamp],
    [dbo].[users].[profile_picture_path],
    [dbo].[users].[profile_picture_blob]
FROM [dbo].[users] (NOLOCK)
    INNER JOIN [dbo].[users_logins] (NOLOCK) ON [dbo].[users_logins].[user_id] = [dbo].[users].[user_id]
WHERE
    [dbo].[users_logins].[login_provider] = @LoginProvider AND
    [dbo].[users_logins].[provider_key] = @ProviderKey