CREATE PROCEDURE [dbo].[AddClaimToRole] (
    @role_id    UNIQUEIDENTIFIER,
    @type       NVARCHAR(256),
    @value      NVARCHAR(256)
) AS
BEGIN
    IF NOT EXISTS(SELECT 1 FROM [dbo].[roles_claims] WHERE [role_id] = @role_id AND [type] = @type)
    BEGIN
        INSERT INTO [dbo].[roles_claims] (
            [role_id],
            [type],
            [value]
        )
        VALUES (
            @role_id,
            @type,
            @value
        );
    END

    RETURN 0
END
GO