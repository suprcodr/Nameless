SELECT
    [role_id],
    [concurrency_stamp],
    [name],
    [normalized_name],
    [owner_id]
FROM
    [dbo].[roles] (NOLOCK)
WHERE
    [name] = @name
