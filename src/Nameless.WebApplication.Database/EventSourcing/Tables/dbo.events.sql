CREATE TABLE [dbo].[events] (
    [event_id]      UNIQUEIDENTIFIER    NOT NULL,
    [aggregate_id]  UNIQUEIDENTIFIER    NOT NULL,
    [version]       INT                 NOT NULL,
    [time_stamp]    DATETIMEOFFSET      NOT NULL,
    [event_type]    NVARCHAR(256)       NOT NULL,
    [payload]       VARBINARY(MAX)      NOT NULL,

    CONSTRAINT [pk_events] PRIMARY KEY ([event_id])
)
GO