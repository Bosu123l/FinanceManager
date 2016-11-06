using Domain.Models;

namespace Domain.Repository
{
    internal class SourceOfAmountRepository : Repository<SourceOfAmount>
    {
        public SourceOfAmountRepository(SessionProvider sessionProvider) : base(sessionProvider.SessionFactory.OpenSession())
        {
        }
    }
}