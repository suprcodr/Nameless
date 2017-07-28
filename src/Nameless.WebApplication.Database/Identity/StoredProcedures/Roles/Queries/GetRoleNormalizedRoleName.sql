SELECT
    [normalized_name]
FROM
    [dbo].[roles] (NOLOCK)
WHERE
    [role_id] = @role_id
