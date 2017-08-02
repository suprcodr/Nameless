CREATE PROCEDURE [dbo].[GetUserPasswordHash] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [password_hash]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO