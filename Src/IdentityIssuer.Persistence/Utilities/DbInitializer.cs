using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IdentityIssuer.Persistence.Utilities
{
    public interface IDbInitializer
    {
        Task Execute();
    }
    
    public class DbInitializer : IDbInitializer
    {
        private readonly AppIdentityContext _context;

        public DbInitializer(AppIdentityContext context)
        {
            this._context = context;
        }

        public async Task Execute()
        {
            await _context.Database.MigrateAsync();
        }
    }
}