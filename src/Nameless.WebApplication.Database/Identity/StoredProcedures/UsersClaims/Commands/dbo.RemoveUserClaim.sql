CREATE PROCEDURE [dbo].[RemoveUserClaim] (
    @user_id    UNIQUEIDENTIFIER,
    @type       NVARCHAR(256)
) AS
BEGIN
    DELETE
    FROM [dbo].[users_claims]
    WHERE
        [user_id] = @user_id
    AND [type] = @type;

    RETURN 0
END
GO