CREATE PROCEDURE [dbo].[IncrementUserAccessFailedCount] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    UPDATE [dbo].[users] SET
        [access_failed_count] = [access_failed_count] + 1
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO