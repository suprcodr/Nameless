-- DROP INDEXES, CONSTRAINTS AND TABLES
IF EXISTS(SELECT 1 FROM sys.tables WHERE [name] = 'users_tokens')
BEGIN
    DROP INDEX [idx_users_tokens_login_provider] ON [dbo].[users_tokens];
    DROP INDEX [idx_users_tokens_name] ON [dbo].[users_tokens];

    ALTER TABLE [dbo].[users_tokens] DROP CONSTRAINT [fk_users_tokens_to_users];
    
    ALTER TABLE [dbo].[users_tokens] DROP CONSTRAINT [pk_users_tokens];

    DROP TABLE [dbo].[users_tokens]
END
GO

IF EXISTS(SELECT 1 FROM sys.tables WHERE [name] = 'users_logins')
BEGIN
    DROP INDEX [idx_users_logins_login_provider] ON [dbo].[users_logins];

    ALTER TABLE [dbo].[users_logins] DROP CONSTRAINT [fk_users_logins_to_users];
    
    ALTER TABLE [dbo].[users_logins] DROP CONSTRAINT [pk_users_logins];

    DROP TABLE [dbo].[users_logins]
END
GO

IF EXISTS(SELECT 1 FROM sys.tables WHERE [name] = 'users_roles')
BEGIN
    ALTER TABLE [dbo].[users_roles] DROP CONSTRAINT [fk_users_roles_to_users];
    ALTER TABLE [dbo].[users_roles] DROP CONSTRAINT [fk_users_roles_to_roles];
    
    DROP TABLE [dbo].[users_roles]
END
GO

IF EXISTS(SELECT 1 FROM sys.tables WHERE [name] = 'roles_claims')
BEGIN
    DROP INDEX [idx_roles_claims_type] ON [dbo].[roles_claims];

    ALTER TABLE [dbo].[roles_claims] DROP CONSTRAINT [fk_roles_claims_to_roles];
    
    ALTER TABLE [dbo].[roles_claims] DROP CONSTRAINT [pk_roles_claims];

    DROP TABLE [dbo].[roles_claims]
END
GO

IF EXISTS(SELECT 1 FROM sys.tables WHERE [name] = 'roles')
BEGIN
    DROP INDEX [idx_roles_name] ON [dbo].[roles];

    ALTER TABLE [dbo].[roles] DROP CONSTRAINT [pk_roles];

    DROP TABLE [dbo].[roles]
END
GO

IF EXISTS(SELECT 1 FROM sys.tables WHERE [name] = 'users_claims')
BEGIN
    DROP INDEX [idx_users_claims_type] ON [dbo].[users_claims];

    ALTER TABLE [dbo].[users_claims] DROP CONSTRAINT [fk_users_claims_to_users];
    
    ALTER TABLE [dbo].[users_claims] DROP CONSTRAINT [pk_users_claims];

    DROP TABLE [dbo].[users_claims]
END
GO

IF EXISTS(SELECT 1 FROM sys.tables WHERE [name] = 'users')
BEGIN
    DROP INDEX [idx_users_user_name] ON [dbo].[users];
    DROP INDEX [idx_users_full_name] ON [dbo].[users];
    DROP INDEX [idx_users_email] ON [dbo].[users];

    ALTER TABLE [dbo].[users] DROP CONSTRAINT [uq_users_user_name];
    ALTER TABLE [dbo].[users] DROP CONSTRAINT [uq_users_email];

    ALTER TABLE [dbo].[users] DROP CONSTRAINT [pk_users];

    DROP TABLE [dbo].[users]
END
GO

-- TABLES, CONSTRAINTS AND INDEXES
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

    CONSTRAINT [pk_users] PRIMARY KEY ([user_id]),

    CONSTRAINT [uq_users_user_name] UNIQUE ([user_name]),
    CONSTRAINT [uq_users_email] UNIQUE ([email])
)
GO

CREATE NONCLUSTERED INDEX [idx_users_user_name] ON [dbo].[users]([user_name])
GO

CREATE NONCLUSTERED INDEX [idx_users_full_name] ON [dbo].[users]([full_name])
GO

CREATE NONCLUSTERED INDEX [idx_users_email] ON [dbo].[users]([email])
GO

CREATE TABLE [dbo].[users_claims] (
    [user_claim_id]	UNIQUEIDENTIFIER    NOT NULL,
    [type]			NVARCHAR(256)       NOT NULL,
    [value]			NVARCHAR(256)       NULL,
    [user_id]		UNIQUEIDENTIFIER    NOT NULL,

    CONSTRAINT [pk_users_claims] PRIMARY KEY ([user_claim_id], [type]),

    CONSTRAINT [fk_users_claims_to_users] FOREIGN KEY ([user_id]) REFERENCES [users]([user_id])
)
GO

CREATE NONCLUSTERED INDEX [idx_users_claims_type] ON [dbo].[users_claims]([type])
GO

CREATE TABLE [dbo].[roles] (
    [role_id]           UNIQUEIDENTIFIER    NOT NULL,
    [concurrency_stamp] NVARCHAR(MAX)       NOT NULL,
    [name]	        	NVARCHAR(256)       NOT NULL,
    [normalized_name]   NVARCHAR(256)       NOT NULL

    CONSTRAINT [pk_roles] PRIMARY KEY ([role_id])
)
GO

CREATE NONCLUSTERED INDEX [idx_roles_name] ON [dbo].[roles]([name])
GO

CREATE NONCLUSTERED INDEX [idx_roles_normalized_name] ON [dbo].[roles]([normalized_name])
GO

CREATE TABLE [dbo].[roles_claims] (
    [role_claim_id]     UNIQUEIDENTIFIER    NOT NULL,
    [type]			    NVARCHAR(256)       NOT NULL,
    [value]			    NVARCHAR(512)       NULL,
    [role_id]		    UNIQUEIDENTIFIER    NOT NULL,

    CONSTRAINT [pk_roles_claims] PRIMARY KEY ([role_claim_id], [type]),

    CONSTRAINT [fk_roles_claims_to_roles] FOREIGN KEY ([role_id]) REFERENCES [roles]([role_id])
)
GO

CREATE NONCLUSTERED INDEX [idx_roles_claims_type] ON [dbo].[roles_claims]([type])
GO

CREATE TABLE [dbo].[users_roles] (
    [user_id]    UNIQUEIDENTIFIER    NOT NULL,
    [role_id]    UNIQUEIDENTIFIER    NOT NULL,

    CONSTRAINT [fk_users_roles_to_users] FOREIGN KEY ([user_id]) REFERENCES [users]([user_id]),
    CONSTRAINT [fk_users_roles_to_roles] FOREIGN KEY ([role_id]) REFERENCES [roles]([role_id])
)
GO

CREATE TABLE [dbo].[users_logins] (
    [user_id]           UNIQUEIDENTIFIER    NOT NULL,
    [login_provider]    NVARCHAR(256)       NOT NULL,
    [provider_key]      NVARCHAR(256)       NULL,
    [display_name]      NVARCHAR(256)       NOT NULL,

    CONSTRAINT [pk_users_logins] PRIMARY KEY ([user_id], [login_provider]),

    CONSTRAINT [fk_users_logins_to_users] FOREIGN KEY ([user_id]) REFERENCES [users]([user_id])
)
GO

CREATE NONCLUSTERED INDEX [idx_users_logins_login_provider] ON [dbo].[users_logins]([login_provider])
GO

CREATE NONCLUSTERED INDEX [idx_users_logins_display_name] ON [dbo].[users_logins]([display_name])
GO

CREATE TABLE [dbo].[users_tokens] (
    [user_id]           UNIQUEIDENTIFIER    NOT NULL,
    [login_provider]    NVARCHAR(256)       NOT NULL,
    [name]              NVARCHAR(128)       NOT NULL,
    [value]             NVARCHAR(128)       NOT NULL,

    CONSTRAINT [pk_users_tokens] PRIMARY KEY ([user_id], [login_provider], [name]),

    CONSTRAINT [fk_users_tokens_to_users] FOREIGN KEY ([user_id]) REFERENCES [users]([user_id])
)
GO

CREATE NONCLUSTERED INDEX [idx_users_tokens_login_provider] ON [dbo].[users_tokens]([login_provider])
GO

CREATE NONCLUSTERED INDEX [idx_users_tokens_name] ON [dbo].[users_tokens]([name])
GO