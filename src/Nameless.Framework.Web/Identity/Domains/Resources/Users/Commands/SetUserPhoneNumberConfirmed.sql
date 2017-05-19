UPDATE [dbo].[users] SET
    [phone_number_confirmed] = @Confirmed
WHERE
    [user_id] = @UserId