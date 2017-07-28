UPDATE [dbo].[users] SET
    [email_confirmed] = @Confirmed
WHERE
    [user_id] = @user_id
