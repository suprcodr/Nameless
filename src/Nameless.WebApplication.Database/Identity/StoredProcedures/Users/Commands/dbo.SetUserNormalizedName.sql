CREATE PROCEDURE [dbo].[SetUserNormalizedName] (
    @user_id                UNIQUEIDENTIFIER,
    @normalized_user_name   NVARCHAR(256)
) AS
BEGIN

    UPDATE [dbo].[users] SET
        [normalized_user_name] = @normalized_user_name
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO