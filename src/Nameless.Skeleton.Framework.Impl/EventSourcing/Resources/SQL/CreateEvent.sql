INSERT INTO [events] (
    [event_id],
    [aggregate_id],
    [version],
    [time_stamp],
    [event_type],
    [payload]
) VALUES (
    @ID,
    @AggregateID,
    @Version,
    @TimeStamp,
    @EventType,
    @Payload
)