SELECT
    [email]
FROM [dbo].[users] (NOLOCK)
WHERE
    [user_id] = @UserId