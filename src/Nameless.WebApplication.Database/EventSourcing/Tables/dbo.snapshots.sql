CREATE TABLE [dbo].[snapshots]
(
    [snapshot_id]   UNIQUEIDENTIFIER    NOT NULL,
    [aggregate_id]  UNIQUEIDENTIFIER    NOT NULL,
    [version]       INT                 NOT NULL,
    [snapshot_type] NVARCHAR(256)       NOT NULL,
    [payload]       VARBINARY(MAX)      NOT NULL,

    CONSTRAINT [pk_snapshot] PRIMARY KEY ([snapshot_id])
)
