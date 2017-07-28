UPDATE [dbo].[users] SET
    [lockout_enabled] = @Enabled
WHERE
    [user_id] = @user_id
