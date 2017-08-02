CREATE PROCEDURE [dbo].[SetUserLockoutEnabled] (
    @user_id            UNIQUEIDENTIFIER,
    @lockout_enabled    BIT
) AS
BEGIN

    UPDATE [dbo].[users] SET
        [lockout_enabled] = @lockout_enabled
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO