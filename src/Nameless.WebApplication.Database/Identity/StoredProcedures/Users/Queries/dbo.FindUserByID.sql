CREATE PROCEDURE [dbo].[FindUserByID] (
    @user_id UNIQUEIDENTIFIER
) AS
BEGIN
    SELECT
        [user_id],
        [concurrency_stamp],
        [user_name],
        [normalized_user_name],
        [full_name],
        [access_failed_count],
        [email],
        [email_confirmed],
        [normalized_email],
        [lockout_enabled],
        [lockout_end_date_utc],
        [password_hash],
        [phone_number],
        [phone_number_confirmed],
        [two_factor_enabled],
        [security_stamp],
        [profile_picture_path],
        [profile_picture_blob],
        [owner_id]
    FROM [dbo].[users] (NOLOCK)
    WHERE
        [user_id] = @user_id;

    RETURN 0
END
GO