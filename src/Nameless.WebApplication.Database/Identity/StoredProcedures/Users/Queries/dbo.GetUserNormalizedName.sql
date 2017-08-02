CREATE PROCEDURE [dbo].[GetUserNormalizedName] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [normalized_user_name]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO