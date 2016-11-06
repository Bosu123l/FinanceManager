using Domain.Models;

namespace Domain.Repository
{
    public class TypeOfOutgoingRepository : Repository<TypeOfOutgoing>
    {
        public TypeOfOutgoingRepository(SessionProvider sessionProvider) : base(sessionProvider.SessionFactory.OpenSession())
        {
        }
    }
}