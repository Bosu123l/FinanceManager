using Domain.Models;

namespace Domain.Repository
{
    public class SourceOfAmountRepository : Repository<SourceOfAmount>
    {
        public SourceOfAmountRepository(SessionProvider sessionProvider) : base(sessionProvider.SessionFactory.OpenSession())
        {
        }
    }
}