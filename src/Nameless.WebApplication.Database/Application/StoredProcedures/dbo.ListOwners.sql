CREATE PROCEDURE [dbo].[ListOwners]
AS
BEGIN
    SELECT
        [owner_id],
        [name]
    FROM [owners] (NOLOCK);

    RETURN 0
END
GO