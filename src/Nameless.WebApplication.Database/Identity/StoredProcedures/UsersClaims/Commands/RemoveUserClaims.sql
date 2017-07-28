DELETE
FROM [dbo].[users_claims]
WHERE
    [dbo].[users_claims].[type] = @type AND
    [dbo].[users_claims].[value] = @value AND
    [dbo].[users_claims].[user_id] = @user_id
