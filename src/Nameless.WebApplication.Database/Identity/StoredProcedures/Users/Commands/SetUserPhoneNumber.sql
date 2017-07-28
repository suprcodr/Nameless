UPDATE [dbo].[users] SET
    [phone_number] = @PhoneNumber
WHERE
    [user_id] = @user_id
