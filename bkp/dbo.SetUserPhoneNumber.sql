CREATE PROCEDURE [dbo].[SetUserPhoneNumber] (
    @user_id        UNIQUEIDENTIFIER,
    @phone_number   NVARCHAR(256)
) AS
BEGIN

    UPDATE [dbo].[users] SET
        [phone_number] = @phone_number,
        [phone_number_confirmed] = 0
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO