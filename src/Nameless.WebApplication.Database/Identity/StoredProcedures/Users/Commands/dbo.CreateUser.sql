CREATE PROCEDURE [dbo].[CreateUser] (
    @user_id                UNIQUEIDENTIFIER,
    @user_name              NVARCHAR(256),
    @email                  NVARCHAR(256),
    @full_name              NVARCHAR(256),
    @profile_picture_path   NVARCHAR(MAX)  = NULL,
    @profile_picture_blob   VARBINARY(MAX) = NULL,
    @owner_id               UNIQUEIDENTIFIER
) AS
BEGIN
    INSERT INTO [dbo].[users] (
        [user_id],
        [user_name],
        [email],
        [full_name],
        [profile_picture_path],
        [profile_picture_blob],
        [owner_id]
    ) VALUES (
        @user_id,
        @user_name,
        @email,
        @full_name,
        @profile_picture_path,
        @profile_picture_blob,
        @owner_id
    );

    RETURN 0
END
GO