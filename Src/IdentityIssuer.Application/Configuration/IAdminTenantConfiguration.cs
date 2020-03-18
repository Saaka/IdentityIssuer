namespace IdentityIssuer.Application.Configuration
{
    public interface IAdminTenantConfiguration
    {
        string Name { get; }
        string Code { get; }
        string UserDisplayName { get; }
        string Email { get; }
        string Password { get; }
        string AllowedOrigin { get; }
    }
}