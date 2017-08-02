CREATE PROCEDURE [dbo].[RemoveRoleClaim] (
    @role_id    UNIQUEIDENTIFIER,
    @type       NVARCHAR(256)
) AS
BEGIN
    DELETE
    FROM [dbo].[roles_claims]
    WHERE
        [role_id] = @role_id
    AND [type] = @type;

    RETURN 0
END
GO