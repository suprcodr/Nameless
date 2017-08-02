CREATE PROCEDURE [dbo].[SetUserLockoutEndDate] (
    @user_id                UNIQUEIDENTIFIER,
    @lockout_end_date_utc   DATETIMEOFFSET
) AS
BEGIN

    UPDATE [dbo].[users] SET
        [lockout_end_date_utc] = @lockout_end_date_utc
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO