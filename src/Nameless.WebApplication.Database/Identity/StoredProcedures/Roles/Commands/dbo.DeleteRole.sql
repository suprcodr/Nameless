CREATE PROCEDURE [dbo].[DeleteRole] (
    @role_id    UNIQUEIDENTIFIER
) AS
BEGIN
    DELETE
    FROM [dbo].[roles]
    WHERE
        [role_id] = @role_id;

    RETURN 0
END
GO