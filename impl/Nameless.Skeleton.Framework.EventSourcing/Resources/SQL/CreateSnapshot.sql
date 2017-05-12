INSERT INTO [snapshots] (
    [snapshot_id],
    [aggregate_id],
    [version],
    [snapshot_type],
    [payload]
) VALUES (
    @ID,
    @AggregateID,
    @Version,
    @SnapshotType,
    @Payload
)