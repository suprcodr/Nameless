CREATE TABLE [dbo].[users_claims] (
    [user_id]   UNIQUEIDENTIFIER    NOT NULL,
    [type]		NVARCHAR(256)       NOT NULL,
    [value]		NVARCHAR(256)       NULL,

    CONSTRAINT [pk_users_claims] PRIMARY KEY ([user_id], [type]),

    CONSTRAINT [fk_users_claims_to_users] FOREIGN KEY ([user_id]) REFERENCES [dbo].[users]([user_id])
)
GO

CREATE NONCLUSTERED INDEX [idx_users_claims_type] ON [dbo].[users_claims]([type])
GO