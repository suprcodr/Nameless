SELECT
    [security_stamp]
FROM [dbo].[users] (NOLOCK)
WHERE
    [user_id] = @UserId