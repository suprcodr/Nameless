SELECT
    [user_id]
FROM [dbo].[users] (NOLOCK)
WHERE
    [normalized_user_name] = @NormalizedUserName