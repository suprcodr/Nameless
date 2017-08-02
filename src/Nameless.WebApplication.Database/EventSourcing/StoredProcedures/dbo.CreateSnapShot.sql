CREATE PROCEDURE [dbo].[CreateSnapShot] (
    @snapshot_id    UNIQUEIDENTIFIER,
    @aggregate_id   UNIQUEIDENTIFIER,
    @version        INT,
    @snapshot_type  NVARCHAR(256),
    @payload        VARBINARY(MAX)
)
AS
BEGIN
    INSERT INTO [dbo].[snapshots] (
        [snapshot_id],
        [aggregate_id],
        [version],
        [snapshot_type],
        [payload]
    ) VALUES (
        @snapshot_id,
        @aggregate_id,
        @version,
        @snapshot_type,
        @payload
    );
    RETURN 0
END
GO