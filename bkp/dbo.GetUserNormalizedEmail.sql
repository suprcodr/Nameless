CREATE PROCEDURE [dbo].[GetUserNormalizedEmail] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [normalized_email]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO