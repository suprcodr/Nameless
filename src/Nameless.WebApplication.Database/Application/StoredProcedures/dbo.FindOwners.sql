CREATE PROCEDURE [dbo].[FindOwners] (
    @name NVARCHAR(256)
) AS
BEGIN
    SELECT
        [owner_id],
        [name]
    FROM [owners] (NOLOCK)
    WHERE
        [name] LIKE '%' + @name + '%';

    RETURN 0
END
GO