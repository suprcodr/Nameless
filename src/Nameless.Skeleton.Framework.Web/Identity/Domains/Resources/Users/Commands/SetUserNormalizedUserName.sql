UPDATE [dbo].[users] SET
    [normalized_user_name] = @NormalizedUserName
WHERE
    [user_id] = @UserId