UPDATE [dbo].[users] SET
    [access_failed_count] = 0
WHERE
    [user_id] = @user_id
