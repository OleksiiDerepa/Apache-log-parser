using LogParser.DAL.Context;
using LogParser.Infrastructure.Validation;

namespace LogParser.BLL.Services
{
    public class BaseService
    {
        protected readonly ApacheLogDbContext _context;

        public BaseService(ApacheLogDbContext context)
        {
            Require.NotNull(context, ()=>nameof(context));
            _context = context;
        }
    }
}