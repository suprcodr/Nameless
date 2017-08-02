CREATE PROCEDURE [dbo].[SetUserNormalizedEmail] (
    @user_id            UNIQUEIDENTIFIER,
    @normalized_email   NVARCHAR(256)
) AS
BEGIN

    UPDATE [dbo].[users] SET
        [normalized_email] = @normalized_email
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO
