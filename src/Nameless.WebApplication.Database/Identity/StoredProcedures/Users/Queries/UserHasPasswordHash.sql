SELECT
    CASE
        WHEN [password_hash] IS NOT NULL THEN 1
        ELSE 0
    END AS [has_password_hash]
FROM [dbo].[users] (NOLOCK)
WHERE
    [user_id] = @user_id
