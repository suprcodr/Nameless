SELECT
    [role_id],
    [concurrency_stamp],
    [name],
    [normalized_name]
FROM
    [dbo].[roles] (NOLOCK)
WHERE
    [role_id] = @RoleId