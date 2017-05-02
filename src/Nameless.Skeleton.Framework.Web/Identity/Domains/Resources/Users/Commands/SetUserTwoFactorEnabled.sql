UPDATE [dbo].[users] SET
    [two_factor_enabled] = @Enabled
WHERE
    [user_id] = @UserId