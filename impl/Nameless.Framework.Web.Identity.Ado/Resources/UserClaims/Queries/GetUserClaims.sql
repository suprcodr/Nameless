SELECT
    [dbo].[users_claims].[user_claim_id],
    [dbo].[users_claims].[type],
    [dbo].[users_claims].[value],
    [dbo].[users].[user_id]
FROM [dbo].[users_claims] (NOLOCK)
    INNER JOIN [dbo].[users] (NOLOCK) ON [dbo].[users].[user_id] = [dbo].[users_claims].[user_id]
WHERE
    [dbo].[users].[user_id] = @UserId
ORDER BY
    [dbo].[users_claims].[type] ASC