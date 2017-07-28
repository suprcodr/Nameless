SELECT
    [role_claim_id],
    [type],
    [value],
    [owner_id]
FROM
    [dbo].[roles_claims] (NOLOCK)
WHERE
    [role_id] = @role_id
