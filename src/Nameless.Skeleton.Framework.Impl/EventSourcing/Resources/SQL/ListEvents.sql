SELECT
    [event_id],
    [aggregate_id],
    [version],
    [time_stamp],
    [event_type],
    [payload]
FROM [events] (NOLOCK)
WHERE [aggregate_id] = @AggregateID AND [version] >= @Version