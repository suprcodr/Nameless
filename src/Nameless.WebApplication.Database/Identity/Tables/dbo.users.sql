CREATE TABLE [dbo].[users] (
    [user_id]                   UNIQUEIDENTIFIER    NOT NULL,
    [concurrency_stamp]         NVARCHAR(MAX)       NOT NULL,
    [user_name]                 NVARCHAR(256)       NOT NULL,
    [normalized_user_name]      NVARCHAR(256)       NULL,
    [full_name]                 NVARCHAR(256)       NOT NULL,
    [access_failed_count]       INT                 NULL,
    [email]                     NVARCHAR(256)       NOT NULL,
    [email_confirmed]           BIT                 NULL,
    [normalized_email]          NVARCHAR(256)       NULL,
    [lockout_enabled]           BIT                 NULL,
    [lockout_end_date_utc]      DATETIMEOFFSET(4)   NULL,
    [password_hash]             NVARCHAR(512)       NULL,
    [phone_number]              NVARCHAR(32)        NULL,
    [phone_number_confirmed]    BIT                 NULL,
    [two_factor_enabled]        BIT                 NULL,
    [security_stamp]            NVARCHAR(256)       NULL,
    [profile_picture_path]      NVARCHAR(MAX)       NULL,
    [profile_picture_blob]      VARBINARY(MAX)      NULL,
    [owner_id]                  UNIQUEIDENTIFIER    NOT NULL,

    CONSTRAINT [pk_users] PRIMARY KEY ([user_id]),

    CONSTRAINT [uq_users_user_name_owner] UNIQUE ([user_name], [owner_id]),
    CONSTRAINT [uq_users_email_owner] UNIQUE ([email], [owner_id]),

    CONSTRAINT [fk_users_to_owners] FOREIGN KEY ([owner_id]) REFERENCES [owners]([owner_id])
)
GO

CREATE NONCLUSTERED INDEX [idx_users_user_name] ON [dbo].[users]([user_name])
GO

CREATE NONCLUSTERED INDEX [idx_users_full_name] ON [dbo].[users]([full_name])
GO

CREATE NONCLUSTERED INDEX [idx_users_email] ON [dbo].[users]([email])
GO