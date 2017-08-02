CREATE PROCEDURE [dbo].[SetUserEmailConfirmed] (
    @user_id            UNIQUEIDENTIFIER,
    @email_confirmed    BIT
) AS
BEGIN

    UPDATE [dbo].[users] SET
        [email_confirmed] = @email_confirmed
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO