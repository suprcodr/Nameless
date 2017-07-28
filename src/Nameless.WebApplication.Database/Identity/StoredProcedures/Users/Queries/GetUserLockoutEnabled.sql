SELECT
    [lockout_enabled]
FROM [dbo].[users] (NOLOCK)
WHERE
    [user_id] = @user_id
