IF EXISTS(SELECT [name] FROM sys.tables WHERE [name] = 'events')
    DROP TABLE [events]

IF EXISTS(SELECT [name] FROM sys.tables WHERE [name] = 'snapshots')
    DROP TABLE [snapshots]

CREATE TABLE [events] (
    [event_id]      UNIQUEIDENTIFIER    NOT NULL,
    [aggregate_id]  UNIQUEIDENTIFIER    NOT NULL,
    [version]       INT                 NOT NULL,
    [time_stamp]    DATETIME            NOT NULL,
    [event_type]    NVARCHAR(256)       NOT NULL,
    [payload]       VARBINARY(MAX)      NOT NULL,

    CONSTRAINT [PK_events] PRIMARY KEY ([event_id])
)
GO

CREATE TABLE [snapshots] (
    [snapshot_id]   UNIQUEIDENTIFIER    NOT NULL,
    [aggregate_id]  UNIQUEIDENTIFIER    NOT NULL,
    [version]       INT                 NOT NULL,
    [snapshot_type] NVARCHAR(256)       NOT NULL,
    [payload]       VARBINARY(MAX)      NOT NULL,

    CONSTRAINT [PK_snapshot] PRIMARY KEY ([snapshot_id])
)
GO