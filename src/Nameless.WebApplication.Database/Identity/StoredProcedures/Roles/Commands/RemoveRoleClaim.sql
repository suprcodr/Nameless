DELETE
FROM [dbo].[roles_claims]
WHERE
    [role_id] = @role_id
AND [type] = @type
AND [value] = @value
