CREATE PROCEDURE [dbo].[AddUserToRole] (
    @user_id    UNIQUEIDENTIFIER,
    @role_name  NVARCHAR(256)
) AS
BEGIN
    DECLARE @owner_id   UNIQUEIDENTIFIER,
            @role_id    UNIQUEIDENTIFIER

    SELECT @owner_id = [owner_id] FROM [dbo].[users] (NOLOCK) WHERE [user_id] = @user_id;
    SELECT @role_id = [role_id] FROM [dbo].[roles] (NOLOCK) WHERE [name] = @role_name AND [owner_id] = @owner_id;

    INSERT INTO [dbo].[users_roles] VALUES (
        @user_id,
        @role_id
    );

    RETURN 0
END
GO