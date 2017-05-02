UPDATE [dbo].[users] SET
    [access_failed_count] = 0
WHERE
    [user_id] = @UserId