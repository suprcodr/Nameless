INSERT INTO [dbo].[users_claims] (
    [user_claim_id],
    [type],
    [value],
    [user_id]
)
VALUES (
    @UserClaimId,
    @Type,
    @Value,
    @UserId
)