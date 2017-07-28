UPDATE [dbo].[roles] SET
    [normalized_name] = @normalized_name
WHERE
    [role_id] = @role_id;
