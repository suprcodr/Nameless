CREATE PROCEDURE [dbo].[AddUserClaim] (
    @user_id    UNIQUEIDENTIFIER,
    @type       NVARCHAR(256),
    @value      NVARCHAR(256)
) AS
BEGIN
    INSERT INTO [dbo].[users_claims] (
        [user_id],
        [type],
        [value]
    )
    VALUES (
        @user_id,
        @type,
        @value
    );

    RETURN 0
END
GO