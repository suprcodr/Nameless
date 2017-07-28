CREATE PROCEDURE [dbo].[RemoveRoleClaims] (
    @role_id    UNIQUEIDENTIFIER,
    @type       NVARCHAR(256)
) AS
BEGIN
    SET NOCOUNT ON

    DELETE
    FROM [dbo].[roles_claims]
    WHERE
        [role_id] = @role_id
    AND [type] = @type;

    SET NOCOUNT OFF

    RETURN 0
END
GO