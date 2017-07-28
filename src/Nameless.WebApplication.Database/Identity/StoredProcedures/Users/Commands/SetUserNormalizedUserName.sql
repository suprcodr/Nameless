UPDATE [dbo].[users] SET
    [normalized_user_name] = @normalized_user_name
WHERE
    [user_id] = @user_id
