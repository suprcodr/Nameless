CREATE PROCEDURE [dbo].[GetUserEmail] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [email]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO