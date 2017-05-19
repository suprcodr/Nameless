SELECT
    [role_claim_id],
    [type],
    [value]
FROM
    [dbo].[roles_claims] (NOLOCK)
WHERE
    [role_id] = @RoleId