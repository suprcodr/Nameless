CREATE PROCEDURE [dbo].[GetUserRoles] (
    @user_id   UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [dbo].[roles].[name],
        [dbo].[roles].[normalized_name]
    FROM [dbo].[users_roles] (NOLOCK)
        INNER JOIN [dbo].[users] (NOLOCK) ON [dbo].[users].[user_id] = [dbo].[users_roles].[user_id]
        INNER JOIN [dbo].[roles] (NOLOCK) ON [dbo].[roles].[role_id] = [dbo].[users_roles].[role_id]
    WHERE
        [dbo].[users].[user_id] = @user_id;

    RETURN 0
END
GO