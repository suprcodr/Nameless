UPDATE [dbo].[users_claims] SET
    [dbo].[users_claims].[type] = @NewType,
    [dbo].[users_claims].[value] = @NewValue
WHERE
    [dbo].[users_claims].[user_id] = @UserId AND
    [dbo].[users_claims].[type] = @OldType AND
    [dbo].[users_claims].[value] = @OldValue