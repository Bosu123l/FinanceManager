using Domain.Models;

namespace Domain.Repository
{
    internal class OutgoingRepository : Repository<Outgoing>
    {
        public OutgoingRepository(SessionProvider sessionProvider) : base(sessionProvider.SessionFactory.OpenSession())
        {
        }
    }
}