CREATE PROCEDURE [dbo].[SetUserSecurityStamp] (
    @user_id        UNIQUEIDENTIFIER,
    @security_stamp NVARCHAR(256)
) AS
BEGIN
    UPDATE [dbo].[users] SET
        [security_stamp] = @security_stamp
    WHERE
        [user_id] = @user_id

    RETURN 0
END
GO