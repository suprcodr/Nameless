INSERT INTO [dbo].[users] (
    [user_id],
    [user_name],
    [email],
    [full_name],
    [profile_picture_path]
) VALUES (
    @user_id,
    @user_name,
    @Email,
    @Fullname,
    @ProfilePicture
)