CREATE PROCEDURE [dbo].[GetUserPhoneNumberConfirmed] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [phone_number_confirmed]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO