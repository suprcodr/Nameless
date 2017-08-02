CREATE PROCEDURE [dbo].[GetUserPhoneNumber] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [phone_number]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO