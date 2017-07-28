SELECT
    [dbo].[users].[user_id],
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
    INNER JOIN [dbo].[users_claims] ON [dbo].[users_claims].[user_id] = [dbo].[users].[user_id]
WHERE
    (@type = NULL OR [dbo].[users_claims].[type] = @type) AND
    (@value = NULL OR [dbo].[users_claims].[value] = @value)
