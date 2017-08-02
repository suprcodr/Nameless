CREATE PROCEDURE [dbo].[UserHasPassword] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    DECLARE @password_hash NVARCHAR(256)

    SELECT
        @password_hash = [password_hash]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [user_id] = @user_id;

    SELECT CASE
        WHEN @password_hash IS NULL OR @password_hash = '' THEN 0
        ELSE 1
    END AS [user_has_password];

    RETURN 0
END
GO