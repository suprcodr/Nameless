SELECT
    [role_id],
    [concurrency_stamp],
    [name],
    [normalized_name]
FROM
    [dbo].[roles] (NOLOCK)
WHERE
    [normalized_name] = @NormalizedName