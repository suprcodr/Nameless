UPDATE [dbo].[users] SET
    [access_failed_count] = [access_failed_count] + 1
WHERE
    [user_id] = @UserId