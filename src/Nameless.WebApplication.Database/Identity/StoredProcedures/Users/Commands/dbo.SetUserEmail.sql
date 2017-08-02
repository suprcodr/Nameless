CREATE PROCEDURE [dbo].[SetUserEmail] (
    @user_id    UNIQUEIDENTIFIER,
    @email      NVARCHAR(256)
) AS
BEGIN
    UPDATE [dbo].[users] SET
        [email] = @email,
        [normalized_email] = NULL,
        [email_confirmed] = 0
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO
