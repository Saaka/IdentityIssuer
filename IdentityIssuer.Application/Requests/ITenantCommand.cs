namespace IdentityIssuer.Application.Requests
{
    public interface ITenantCommand
    {
        int TenantId { get; set; }
    }
}