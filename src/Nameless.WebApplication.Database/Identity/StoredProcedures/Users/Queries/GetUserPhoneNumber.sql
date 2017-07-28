SELECT
    [phone_number]
FROM [dbo].[users] (NOLOCK)
WHERE
    [user_id] = @user_id
