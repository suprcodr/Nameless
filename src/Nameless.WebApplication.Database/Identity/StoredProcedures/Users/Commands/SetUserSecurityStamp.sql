UPDATE [dbo].[users] SET
    [security_stamp] = @SecurityStamp
WHERE
    [user_id] = @user_id
