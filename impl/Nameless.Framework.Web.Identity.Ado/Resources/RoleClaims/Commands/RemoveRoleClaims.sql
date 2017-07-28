DELETE
FROM [dbo].[roles_claims]
WHERE
    [dbo].[roles_claims].[type] = @type AND
    [dbo].[roles_claims].[value] = @value AND
    [dbo].[roles_claims].[role_id] = @role_id