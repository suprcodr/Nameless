CREATE PROCEDURE [dbo].[AddUserLogin] (
    @user_id        UNIQUEIDENTIFIER,
    @login_provider NVARCHAR(256),
    @provider_key   NVARCHAR(256),
    @display_name   NVARCHAR(256)
) AS
BEGIN
    INSERT INTO [dbo].[users_logins] (
        [user_id],
        [login_provider],
        [provider_key],
        [display_name]
    ) VALUES (
        @user_id,
        @login_provider,
        @provider_key,
        @display_name
    );

    RETURN 0
END
GO