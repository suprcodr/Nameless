UPDATE [dbo].[users] SET
    [lockout_end_date] = @LockoutEndDate
WHERE
    [user_id] = @user_id
