SELECT
    [snapshot_id],
    [aggregate_id],
    [version],
    [snapshot_type],
    [payload]
FROM [snapshots] (NOLOCK)
WHERE [aggregate_id] = @AggregateID