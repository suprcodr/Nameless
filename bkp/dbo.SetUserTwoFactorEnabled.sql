CREATE PROCEDURE [dbo].[SetUserTwoFactorEnabled] (
    @user_id            UNIQUEIDENTIFIER,
    @two_factor_enabled BIT
) AS
BEGIN
    UPDATE [dbo].[users] SET
        [two_factor_enabled] = @two_factor_enabled
    WHERE
        [user_id] = @user_id

    RETURN 0
END
GO