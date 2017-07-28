UPDATE [dbo].[roles] SET
    [name] = @name,
    [normalized_name] = @normalized_name
WHERE
    [role_id] = @role_id;
