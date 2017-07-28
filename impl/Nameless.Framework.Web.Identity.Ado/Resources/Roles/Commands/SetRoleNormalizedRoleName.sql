UPDATE [dbo].[roles] SET
    [normalized_name] = @NormalizedName
WHERE [role_id] = @RoleId;