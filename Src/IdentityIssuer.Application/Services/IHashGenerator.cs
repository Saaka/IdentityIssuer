namespace IdentityIssuer.Application.Services
{
    public interface IHashGenerator
    {
        string Generate(string value);
    }
}