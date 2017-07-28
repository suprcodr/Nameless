UPDATE [dbo].[users] SET
    [password_hash] = @PasswordHash
WHERE
    [user_id] = @user_id
