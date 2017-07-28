UPDATE [dbo].[users] SET
    [full_name] = @Fullname,
    [profile_picture_path] = @ProfilePicture
WHERE
    [user_id] = @user_id
