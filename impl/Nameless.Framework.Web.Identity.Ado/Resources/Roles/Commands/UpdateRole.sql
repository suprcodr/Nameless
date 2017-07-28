UPDATE [dbo].[roles] SET
    [name] = @Name,
    [normalized_name] = @NormalizedName
WHERE [role_id] = @RoleId;