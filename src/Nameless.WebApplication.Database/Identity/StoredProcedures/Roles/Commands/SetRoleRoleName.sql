UPDATE [dbo].[roles] SET
    [name] = @name
WHERE
    [role_id] = @role_id;
