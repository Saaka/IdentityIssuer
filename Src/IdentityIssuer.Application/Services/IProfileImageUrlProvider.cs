namespace IdentityIssuer.Application.Services
{
    public interface IProfileImageUrlProvider
    {
        string GetImageUrl(string email);
    }
}