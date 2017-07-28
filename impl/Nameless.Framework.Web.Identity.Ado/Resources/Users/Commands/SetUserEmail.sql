UPDATE [dbo].[users] SET
    [email] = @Email,
    [normalized_email] = NULL,
    [email_confirmed] = 0
WHERE
    [user_id] = @UserId