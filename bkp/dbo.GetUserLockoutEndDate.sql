CREATE PROCEDURE [dbo].[GetUserLockoutEndDate] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [lockout_end_date_utc]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO