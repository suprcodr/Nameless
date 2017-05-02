SELECT
    [dbo].[roles_claims].[role_claim_id],
    [dbo].[roles_claims].[type],
    [dbo].[roles_claims].[value],
    [dbo].[roles].[role_id]
FROM [dbo].[roles_claims] (NOLOCK)
    INNER JOIN [dbo].[roles] (NOLOCK) ON [dbo].[roles].[role_id] = [dbo].[roles_claims].[role_id]
WHERE
    [dbo].[roles].[role_id] = @RoleId
ORDER BY
    [dbo].[roles_claims].[type] ASC