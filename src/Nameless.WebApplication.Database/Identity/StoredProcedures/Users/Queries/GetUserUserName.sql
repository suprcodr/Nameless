SELECT
    [user_name]
FROM [dbo].[users] (NOLOCK)
WHERE
    [user_id] = @user_id
