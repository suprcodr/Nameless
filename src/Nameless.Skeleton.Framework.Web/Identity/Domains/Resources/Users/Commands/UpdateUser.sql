UPDATE [dbo].[users] SET
    [full_name] = @FullName,
    [profile_picture_path] = @ProfilePicture
WHERE
    [user_id] = @UserId