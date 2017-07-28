CREATE TABLE [dbo].[roles_claims] (
    [role_id]   UNIQUEIDENTIFIER    NOT NULL,
    [type]      NVARCHAR(256)       NOT NULL,
    [value]     NVARCHAR(512)       NULL,

    CONSTRAINT [uq_roles_claims_role_type] UNIQUE ([role_id], [type]),

    CONSTRAINT [fk_roles_claims_to_roles] FOREIGN KEY ([role_id]) REFERENCES [dbo].[roles]([role_id])
)
GO

CREATE NONCLUSTERED INDEX [idx_roles_claims_type] ON [dbo].[roles_claims]([type])
GO