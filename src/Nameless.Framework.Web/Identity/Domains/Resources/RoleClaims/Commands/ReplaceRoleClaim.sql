UPDATE [dbo].[roles_claims] SET
    [dbo].[roles_claims].[type] = @NewType,
    [dbo].[roles_claims].[value] = @NewValue
WHERE
    [dbo].[roles_claims].[role_id] = @RoleId AND
    [dbo].[roles_claims].[type] = @OldType AND
    [dbo].[roles_claims].[value] = @OldValue