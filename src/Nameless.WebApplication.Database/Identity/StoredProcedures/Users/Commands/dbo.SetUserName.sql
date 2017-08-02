CREATE PROCEDURE [dbo].[SetUserName] (
    @user_id    UNIQUEIDENTIFIER,
    @user_name  NVARCHAR(256)
) AS
BEGIN

    UPDATE [dbo].[users] SET
        [user_name] = @user_name,
        [normalized_user_name] = NULL
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO