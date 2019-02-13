namespace QrF.Core.IdentityServer4.Dapper.Entities
{
    public abstract class UserClaim
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
