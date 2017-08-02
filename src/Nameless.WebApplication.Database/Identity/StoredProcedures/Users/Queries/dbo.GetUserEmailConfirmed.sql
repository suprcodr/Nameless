CREATE PROCEDURE [dbo].[GetUserEmailConfirmed] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [email_confirmed]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO