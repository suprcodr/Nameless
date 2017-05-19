INSERT INTO [dbo].[roles_claims] (
    [role_claim_id],
    [type],
    [value],
    [role_id]
)
VALUES (
    @RoleClaimId,
    @Type,
    @Value,
    @RoleId
)