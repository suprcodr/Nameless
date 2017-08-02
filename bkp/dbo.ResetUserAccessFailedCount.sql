CREATE PROCEDURE [dbo].[ResetUserAccessFailedCount] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    UPDATE [dbo].[users] SET
        [access_failed_count] = 0
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO