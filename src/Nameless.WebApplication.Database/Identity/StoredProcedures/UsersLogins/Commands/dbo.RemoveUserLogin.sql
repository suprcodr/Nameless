CREATE PROCEDURE [dbo].[RemoveUserLogin] (
    @user_id        UNIQUEIDENTIFIER,
    @login_provider NVARCHAR(256)
) AS
BEGIN
    DELETE
    FROM [dbo].[users_logins]
    WHERE
        [user_id] = @user_id
    AND [login_provider] = @login_provider;

    RETURN 0
END
GO