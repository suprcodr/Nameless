UPDATE [dbo].[users] SET
    [normalized_email] = @NormalizedEmail
WHERE
    [user_id] = @UserId