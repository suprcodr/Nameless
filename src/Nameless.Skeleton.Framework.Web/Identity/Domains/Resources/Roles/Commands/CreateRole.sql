INSERT INTO [dbo].[roles] (
    [role_id],
    [concurrency_stamp],
    [name],
    [normalized_name]
) VALUES (
    @RoleId,
    @ConcurrencyStamp,
    @Name,
    @NormalizedName
);