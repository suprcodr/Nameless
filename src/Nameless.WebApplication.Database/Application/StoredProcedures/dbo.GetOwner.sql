CREATE PROCEDURE [dbo].[GetOwner] (
    @owner_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [owner_id],
        [name]
    FROM [owners] (NOLOCK)
    WHERE
        [owner_id] = @owner_id;

    RETURN 0
END
GO