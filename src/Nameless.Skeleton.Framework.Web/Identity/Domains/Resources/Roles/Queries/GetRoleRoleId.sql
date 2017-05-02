SELECT
    [role_id]
FROM
    [dbo].[roles] (NOLOCK)
WHERE
    [normalized_name] = @NormalizedName