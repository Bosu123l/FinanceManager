using Domain.Models;

namespace Domain.Repository
{
    internal class TypeOfOutgoingRepository : Repository<TypeOfOutgoing>
    {
        public TypeOfOutgoingRepository(SessionProvider sessionProvider) : base(sessionProvider.SessionFactory.OpenSession())
        {
        }
    }
}