CREATE TABLE [dbo].[users_tokens] (
    [user_id]           UNIQUEIDENTIFIER    NOT NULL,
    [login_provider]    NVARCHAR(256)       NOT NULL,
    [name]              NVARCHAR(128)       NOT NULL,
    [value]             NVARCHAR(128)       NOT NULL,

    CONSTRAINT [pk_users_tokens] PRIMARY KEY ([user_id], [login_provider], [name]),

    CONSTRAINT [fk_users_tokens_to_users] FOREIGN KEY ([user_id]) REFERENCES [users]([user_id])
)
GO

CREATE NONCLUSTERED INDEX [idx_users_tokens_login_provider] ON [dbo].[users_tokens]([login_provider])
GO

CREATE NONCLUSTERED INDEX [idx_users_tokens_name] ON [dbo].[users_tokens]([name])
GO