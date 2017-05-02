UPDATE [dbo].[users] SET
    [security_stamp] = @SecurityStamp
WHERE
    [user_id] = @UserId