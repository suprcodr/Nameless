CREATE PROCEDURE [dbo].[GetUserID] (
    @normalized_user_name   NVARCHAR(256),
    @owner_id               UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [user_id]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [normalized_user_name] = @normalized_user_name
    AND [owner_id] = @owner_id;

    RETURN 0
END
GO