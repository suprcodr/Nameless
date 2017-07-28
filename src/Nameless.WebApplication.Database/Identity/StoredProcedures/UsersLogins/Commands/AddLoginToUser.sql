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
)