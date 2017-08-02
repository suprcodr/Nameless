CREATE PROCEDURE [dbo].[GetUserSecurityStamp] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [security_stamp]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO