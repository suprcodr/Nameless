UPDATE [dbo].[users] SET
    [user_name] = @user_name,
    [normalized_user_name] = NULL
WHERE
    [user_id] = @user_id
