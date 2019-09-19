namespace IdentityIssuer.Persistence.Entities
{
    public class AllowedOrigin
    {
        public int AllowedOriginId { get; set; }
        public int TenantId { get; set; }
        public string Value { get; set; }
    }
}
