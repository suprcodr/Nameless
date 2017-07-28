UPDATE [dbo].[roles_claims] SET
    [dbo].[roles_claims].[type] = @new_type,
    [dbo].[roles_claims].[value] = @new_value
WHERE
    [dbo].[roles_claims].[role_id] = @role_id AND
    [dbo].[roles_claims].[type] = @old_type AND
    [dbo].[roles_claims].[value] = @old_value