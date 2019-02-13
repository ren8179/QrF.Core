namespace QrF.Core.IdentityServer4.Dapper.Entities
{
    public class ClientScope
    {
        public int Id { get; set; }
        public string Scope { get; set; }
        public Client Client { get; set; }
    }
}
