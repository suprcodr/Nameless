CREATE PROCEDURE [dbo].[GetUserName] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [user_name]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO