CREATE PROCEDURE [dbo].[CreateEvent] (
    @event_id       UNIQUEIDENTIFIER,
    @aggregate_id   UNIQUEIDENTIFIER,
    @version        INT,
    @time_stamp     DATETIMEOFFSET,
    @event_type     NVARCHAR(256),
    @payload        VARBINARY(MAX)
)
AS
BEGIN
    INSERT INTO [dbo].[events] (
        [event_id],
        [aggregate_id],
        [version],
        [time_stamp],
        [event_type],
        [payload]
    ) VALUES (
        @event_id,
        @aggregate_id,
        @version,
        @time_stamp,
        @event_type,
        @payload
    );

    RETURN 0
END
GO