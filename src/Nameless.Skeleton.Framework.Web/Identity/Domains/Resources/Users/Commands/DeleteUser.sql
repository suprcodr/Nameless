DELETE FROM [dbo].[users_logins]
WHERE [user_id] = @UserId
GO

DELETE FROM [dbo].[users_claims]
WHERE [user_id] = @UserId
GO

DELETE FROM [dbo].[users]
WHERE [user_id] = @UserId
GO