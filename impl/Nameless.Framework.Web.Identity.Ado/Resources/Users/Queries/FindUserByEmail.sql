SELECT
    [user_id],
    [user_name],
    [normalized_user_name],
    [full_name],
    [access_failed_count],
    [email],
    [email_confirmed],
    [normalized_email],
    [lockout_enabled],
    [lockout_end_date_utc],
    [password_hash],
    [phone_number],
    [phone_number_confirmed],
    [two_factor_enabled],
    [security_stamp],
    [profile_picture_path],
    [profile_picture_blob]
FROM [dbo].[users] (NOLOCK)
WHERE
    [normalized_email] = @NormalizedEmail