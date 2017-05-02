SELECT
    [phone_number_confirmed]
FROM [dbo].[users] (NOLOCK)
WHERE
    [user_id] = @UserId