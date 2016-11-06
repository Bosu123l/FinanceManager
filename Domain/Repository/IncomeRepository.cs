using Domain.Models;

namespace Domain.Repository
{
    public class IncomeRepository : Repository<Income>
    {
        public IncomeRepository(SessionProvider sessionProvider) : base(sessionProvider.SessionFactory.OpenSession())
        {
        }
    }
}