UPDATE [dbo].[users_claims] SET
    [dbo].[users_claims].[type] = @new_type,
    [dbo].[users_claims].[value] = @new_value
WHERE
    [dbo].[users_claims].[user_id] = @user_id AND
    [dbo].[users_claims].[type] = @old_type AND
    [dbo].[users_claims].[value] = @old_value