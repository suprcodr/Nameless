UPDATE [dbo].[users] SET
    [user_name] = @UserName,
    [normalized_user_name] = NULL
WHERE
    [user_id] = @UserId