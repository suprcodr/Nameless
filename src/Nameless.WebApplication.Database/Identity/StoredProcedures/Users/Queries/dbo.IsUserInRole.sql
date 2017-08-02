CREATE PROCEDURE [dbo].[IsUserInRole] (
    @user_id   UNIQUEIDENTIFIER,
    @role_name  NVARCHAR(256)
) AS
BEGIN
    DECLARE @result BIT

    SELECT
        @result = 1
    FROM [dbo].[users_roles] (NOLOCK)
        INNER JOIN [dbo].[users] (NOLOCK) ON [dbo].[users].[user_id] = [dbo].[users_roles].[user_id]
        INNER JOIN [dbo].[roles] (NOLOCK) ON [dbo].[roles].[role_id] = [dbo].[users_roles].[role_id]
    WHERE
        [dbo].[users].[user_id] = @user_id
    AND [dbo].[roles].[name] = @role_name;

    SELECT ISNULL(@result, 0) AS [is_user_in_role];

    RETURN 0
END
GO