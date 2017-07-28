UPDATE [dbo].[users] SET
    [normalized_email] = @normalized_Email
WHERE
    [user_id] = @user_id
