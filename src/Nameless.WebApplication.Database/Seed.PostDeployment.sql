/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/
--Reference Data for owners 
MERGE INTO [dbo].[owners] AS [target]
    USING (VALUES (
        '00000000-0000-0000-0000-000000000000', -- owner_id
        N'Administrator'                        -- name
    )) AS [source] (
        [owner_id],
        [name]
    )
    ON [target].[owner_id] = [source].[owner_id]

    -- update matched rows 
    WHEN MATCHED THEN
        UPDATE SET [name] = [source].[name]
    -- insert new rows 
    WHEN NOT MATCHED THEN
        INSERT (
            [owner_id],
            [name]
        ) VALUES (
            [owner_id],
            [name]
        )
    -- delete rows that are in the target but not the source 
    WHEN NOT MATCHED BY SOURCE THEN DELETE;

--Reference Data for users
MERGE INTO [dbo].[users] AS [target]
    USING (VALUES (
        '00000000-0000-0000-0000-000000000000', -- user_id
        NULL,                                   -- concurrency_stamp
        'Administrator',                        -- user_name
        'Administrator',                        -- normalized_user_name
        'Administrator',                        -- full_name
        0,                                      -- access_failed_count
        'administrator@application.com',        -- email
        1,                                      -- email_confirmed
        'administrator@application.com',        -- normalized_email
        0,                                      -- lockout_enabled
        NULL,                                   -- lockout_end_date_utc
        NULL,                                   -- password_hash
        NULL,                                   -- phone_number
        1,                                      -- phone_number_confirmed
        0,                                      -- two_factor_enabled
        NULL,                                   -- security_stamp
        NULL,                                   -- profile_picture_path
        NULL,                                   -- profile_picture_blob
        '00000000-0000-0000-0000-000000000000'  -- owner_id
    )) AS [source] (
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
    )
    ON [target].[user_id] = [source].[user_id]

    -- update matched rows 
    WHEN MATCHED THEN
        UPDATE SET  [concurrency_stamp]         = [source].[concurrency_stamp],
                    [user_name]                 = [source].[user_name],
                    [normalized_user_name]      = [source].[normalized_user_name],
                    [full_name]                 = [source].[full_name],
                    [access_failed_count]       = [source].[access_failed_count],
                    [email]                     = [source].[email],
                    [email_confirmed]           = [source].[email_confirmed],
                    [normalized_email]          = [source].[normalized_email],
                    [lockout_enabled]           = [source].[lockout_enabled],
                    [lockout_end_date_utc]      = [source].[lockout_end_date_utc],
                    [password_hash]             = [source].[password_hash],
                    [phone_number]              = [source].[phone_number],
                    [phone_number_confirmed]    = [source].[phone_number_confirmed],
                    [two_factor_enabled]        = [source].[two_factor_enabled],
                    [security_stamp]            = [source].[security_stamp],
                    [profile_picture_path]      = [source].[profile_picture_path],
                    [profile_picture_blob]      = [source].[profile_picture_blob],
                    [owner_id]                  = [source].[owner_id]
    -- insert new rows 
    WHEN NOT MATCHED THEN
        INSERT (
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
        ) VALUES (
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
        )
    -- delete rows that are in the target but not the source 
    WHEN NOT MATCHED BY SOURCE THEN DELETE;

--Reference Data for roles
MERGE INTO [dbo].[roles] AS [target]
    USING (VALUES (
        '00000000-0000-0000-0000-000000000000', -- role_id
        NULL,                                   -- concurrency_stamp
        N'Administrator',                       -- name
        N'Administrator',                       -- normalized_name
        '00000000-0000-0000-0000-000000000000'  -- owner_id
    )) AS [source] (
        [role_id],
        [concurrency_stamp],
        [name],
        [normalized_name],
        [owner_id]
    )
    ON [target].[role_id] = [source].[role_id]

    -- update matched rows 
    WHEN MATCHED THEN
        UPDATE SET  [concurrency_stamp] = [source].[concurrency_stamp],
                    [name]              = [source].[name],
                    [normalized_name]   = [source].[normalized_name],
                    [owner_id]          = [source].[owner_id]
    -- insert new rows 
    WHEN NOT MATCHED THEN
        INSERT (
            [role_id],
            [concurrency_stamp],
            [name],
            [normalized_name],
            [owner_id]
        ) VALUES (
            [role_id],
            [concurrency_stamp],
            [name],
            [normalized_name],
            [owner_id]
        )
    -- delete rows that are in the target but not the source 
    WHEN NOT MATCHED BY SOURCE THEN DELETE;

--Reference Data for users_roles
MERGE INTO [dbo].[users_roles] AS [target]
    USING (VALUES (
        '00000000-0000-0000-0000-000000000000', -- user_id
        '00000000-0000-0000-0000-000000000000'  -- role_id
    )) AS [source] (
        [user_id],
        [role_id]
    )
    ON [target].[user_id] = [source].[user_id] AND [target].[role_id] = [source].[role_id]

    -- update matched rows 
    WHEN MATCHED THEN
        UPDATE SET  [user_id] = [source].[user_id],
                    [role_id] = [source].[role_id]
    -- insert new rows 
    WHEN NOT MATCHED THEN
        INSERT (
            [user_id],
            [role_id]
        ) VALUES (
            [user_id],
            [role_id]
        )
    -- delete rows that are in the target but not the source 
    WHEN NOT MATCHED BY SOURCE THEN DELETE;