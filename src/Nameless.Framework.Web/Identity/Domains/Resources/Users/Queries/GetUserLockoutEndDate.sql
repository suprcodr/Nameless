SELECT
    [lockout_end_date_utc]
FROM [dbo].[users] (NOLOCK)
WHERE
    [user_id] = @UserId