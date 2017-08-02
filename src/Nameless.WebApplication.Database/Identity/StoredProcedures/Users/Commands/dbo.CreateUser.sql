CREATE PROCEDURE [dbo].[CreateUser] (
    @user_id                UNIQUEIDENTIFIER,
    @user_name              NVARCHAR(256),
    @email                  NVARCHAR(256),
    @full_name              NVARCHAR(256),
    @profile_picture_path   NVARCHAR(MAX)  = NULL,
    @profile_picture_blob   VARBINARY(MAX) = NULL
) AS
BEGIN
    INSERT INTO [dbo].[users] (
        [user_id],
        [user_name],
        [email],
        [full_name],
        [profile_picture_path],
        [profile_picture_blob]
    ) VALUES (
        @user_id,
        @user_name,
        @email,
        @full_name,
        @profile_picture_path,
        @profile_picture_blob
    );

    RETURN 0
END
GO