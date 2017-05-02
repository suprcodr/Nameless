DELETE
FROM [dbo].[roles_claims]
WHERE
    [dbo].[roles_claims].[type] = @Type AND
    [dbo].[roles_claims].[value] = @Value AND
    [dbo].[roles_claims].[role_id] = @RoleId