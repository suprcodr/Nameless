CREATE PROCEDURE [dbo].[ReplaceUserClaim] (
    @user_id    UNIQUEIDENTIFIER,
    @old_type   NVARCHAR(256),
    @new_type   NVARCHAR(256),
    @new_value  NVARCHAR(256)
) AS
BEGIN
    UPDATE [dbo].[users_claims] SET
        [type] = @new_type,
        [value] = @new_value
    WHERE
        [user_id] = @user_id
    AND [type] = @old_type;

    RETURN 0
END
GO