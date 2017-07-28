CREATE PROCEDURE [dbo].[GetSnapShot] (
    @aggregate_id UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT
        [snapshot_id],
        [aggregate_id],
        [version],
        [snapshot_type],
        [payload]
    FROM [dbo].[snapshots] (NOLOCK)
    WHERE
        [aggregate_id] = @aggregate_id;

    RETURN 0
END