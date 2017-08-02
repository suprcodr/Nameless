CREATE PROCEDURE [dbo].[UpdateUser] (
    @user_id                UNIQUEIDENTIFIER,
    @full_name              NVARCHAR(256),
    @profile_picture_path   NVARCHAR(MAX)  = NULL,
    @profile_picture_blob   VARBINARY(MAX) = NULL
) AS
BEGIN
    UPDATE [dbo].[users] SET
        [full_name] = @full_name,
        [profile_picture_path] = @profile_picture_path,
        [profile_picture_blob] = @profile_picture_blob
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO