CREATE TABLE [dbo].[roles] (
    [role_id]           UNIQUEIDENTIFIER    NOT NULL,
    [concurrency_stamp] NVARCHAR(MAX)       NULL,
    [name]	        	NVARCHAR(256)       NOT NULL,
    [normalized_name]   NVARCHAR(256)       NULL,
    [owner_id]          UNIQUEIDENTIFIER    NOT NULL,

    CONSTRAINT [pk_roles] PRIMARY KEY ([role_id]),

    CONSTRAINT [uq_roles_name_owner] UNIQUE ([name], [owner_id]),

    CONSTRAINT [fk_roles_to_owners] FOREIGN KEY ([owner_id]) REFERENCES [owners]([owner_id])
)
GO

CREATE NONCLUSTERED INDEX [idx_roles_name] ON [dbo].[roles]([name])
GO

CREATE NONCLUSTERED INDEX [idx_roles_normalized_name] ON [dbo].[roles]([normalized_name])
GO