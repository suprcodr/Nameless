CREATE PROCEDURE [dbo].[SetUserPhoneNumberConfirmed] (
    @user_id                UNIQUEIDENTIFIER,
    @phone_number_confirmed BIT
) AS
BEGIN

    UPDATE [dbo].[users] SET
        [phone_number_confirmed] = @phone_number_confirmed
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO