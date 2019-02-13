namespace QrF.Core.IdentityServer4.Dapper.Entities
{
    public class ClientSecret : Secret
    {
        public Client Client { get; set; }
    }
}
