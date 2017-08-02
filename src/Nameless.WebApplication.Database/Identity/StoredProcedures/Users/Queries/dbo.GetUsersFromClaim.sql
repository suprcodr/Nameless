CREATE PROCEDURE [dbo].[GetUsersFromClaim] (
    @type       NVARCHAR(256),
    @owner_id   UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [dbo].[users].[user_id],
        [dbo].[users].[concurrency_stamp],
        [dbo].[users].[user_name],
        [dbo].[users].[normalized_user_name],
        [dbo].[users].[full_name],
        [dbo].[users].[access_failed_count],
        [dbo].[users].[email],
        [dbo].[users].[email_confirmed],
        [dbo].[users].[normalized_email],
        [dbo].[users].[lockout_enabled],
        [dbo].[users].[lockout_end_date_utc],
        [dbo].[users].[password_hash],
        [dbo].[users].[phone_number],
        [dbo].[users].[phone_number_confirmed],
        [dbo].[users].[two_factor_enabled],
        [dbo].[users].[security_stamp],
        [dbo].[users].[profile_picture_path],
        [dbo].[users].[profile_picture_blob],
        [dbo].[users].[owner_id]
    FROM [dbo].[users] (NOLOCK)
        INNER JOIN [dbo].[users_claims] (NOLOCK) ON [dbo].[users_claims].[user_id] = [dbo].[users].[user_id]
    WHERE
        [dbo].[users].[owner_id] = @owner_id
    AND [dbo].[users_claims].[type] = @type;

    RETURN 0
END
GO