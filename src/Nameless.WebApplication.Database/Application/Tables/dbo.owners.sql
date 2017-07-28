CREATE TABLE [dbo].[owners]
(
    [owner_id]  UNIQUEIDENTIFIER    NOT NULL, 
    [name]      NVARCHAR(256)       NOT NULL,

    CONSTRAINT [pk_owners] PRIMARY KEY ([owner_id]),

    CONSTRAINT [uq_owners_name] UNIQUE ([name]) 
)
GO

CREATE NONCLUSTERED INDEX [idx_owners_name] ON [dbo].[owners] ([name])
