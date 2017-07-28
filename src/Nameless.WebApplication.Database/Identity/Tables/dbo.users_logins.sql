CREATE TABLE [dbo].[users_logins] (
    [user_id]           UNIQUEIDENTIFIER    NOT NULL,
    [login_provider]    NVARCHAR(256)       NOT NULL,
    [provider_key]      NVARCHAR(256)       NULL,
    [display_name]      NVARCHAR(256)       NOT NULL,

    CONSTRAINT [pk_users_logins] PRIMARY KEY ([user_id], [login_provider]),

    CONSTRAINT [fk_users_logins_to_users] FOREIGN KEY ([user_id]) REFERENCES [users]([user_id])
)
GO

CREATE NONCLUSTERED INDEX [idx_users_logins_login_provider] ON [dbo].[users_logins]([login_provider])
GO

CREATE NONCLUSTERED INDEX [idx_users_logins_display_name] ON [dbo].[users_logins]([display_name])
GO