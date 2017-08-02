CREATE PROCEDURE [dbo].[ListEvents] (
    @aggregate_id   UNIQUEIDENTIFIER,
    @version        INT
)
AS
BEGIN
    SELECT
        [event_id],
        [aggregate_id],
        [version],
        [time_stamp],
        [event_type],
        [payload]
    FROM [dbo].[events] (NOLOCK)
    WHERE
        [aggregate_id] = @aggregate_id
    AND [version] >= @version;

    RETURN 0
END