namespace IdentityIssuer.Application.Configuration
{
    public interface IAdminTenantConfiguration
    {
        string Code { get; }
        string Email { get; }
        string Password { get; }
        string AllowedOrigin { get; }
    }
}