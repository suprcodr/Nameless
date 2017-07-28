DELETE
FROM [dbo].[users_claims]
WHERE
    [dbo].[users_claims].[type] = @Type AND
    [dbo].[users_claims].[value] = @Value AND
    [dbo].[users_claims].[user_id] = @UserId