CREATE TABLE [dbo].[users_roles] (
    [user_id]   UNIQUEIDENTIFIER    NOT NULL,
    [role_id]   UNIQUEIDENTIFIER    NOT NULL,

    CONSTRAINT [fk_users_roles_to_users] FOREIGN KEY ([user_id]) REFERENCES [users]([user_id]),
    CONSTRAINT [fk_users_roles_to_roles] FOREIGN KEY ([role_id]) REFERENCES [roles]([role_id])
)
GO