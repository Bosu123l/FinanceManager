using System.Collections.Generic;

namespace Domain
{
    public interface IPersonRepository
    {
        IEnumerable<Person> Persons { get; set; }

        void Add(Person person);

        void Remove(Person person);
    }
}