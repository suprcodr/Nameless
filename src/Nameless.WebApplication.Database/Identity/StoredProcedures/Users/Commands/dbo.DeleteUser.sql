CREATE PROCEDURE [dbo].[DeleteUser] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    DELETE
    FROM [dbo].[users_roles]
    WHERE [user_id] = @user_id;

    DELETE
    FROM [dbo].[users_tokens]
    WHERE [user_id] = @user_id;

    DELETE
    FROM [dbo].[users_logins]
    WHERE [user_id] = @user_id;

    DELETE
    FROM [dbo].[users_claims]
    WHERE [user_id] = @user_id;

    DELETE
    FROM [dbo].[users]
    WHERE [user_id] = @user_id;

    RETURN 0
END
GO