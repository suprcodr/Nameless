SELECT
    [normalized_user_name]
FROM [dbo].[users] (NOLOCK)
WHERE
    [user_id] = @UserId