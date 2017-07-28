SELECT
    [access_failed_count]
FROM [dbo].[users] (NOLOCK)
WHERE
    [user_id] = @user_id
