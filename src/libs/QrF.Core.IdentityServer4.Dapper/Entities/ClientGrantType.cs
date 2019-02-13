namespace QrF.Core.IdentityServer4.Dapper.Entities
{
    public class ClientGrantType
    {
        public int Id { get; set; }
        public string GrantType { get; set; }
        public Client Client { get; set; }
    }
}
