SELECT
    [normalized_email]
FROM [dbo].[users] (NOLOCK)
WHERE
    [user_id] = @UserId