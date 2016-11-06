using Domain.Models;

namespace Domain.Repository
{
    public class OutgoingRepository : Repository<Outgoing>
    {
        public OutgoingRepository(SessionProvider sessionProvider) : base(sessionProvider.SessionFactory.OpenSession())
        {
        }
    }
}