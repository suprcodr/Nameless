CREATE PROCEDURE [dbo].[GetUserAccessFailedCount] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [access_failed_count]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO