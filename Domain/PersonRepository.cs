using System.Collections.Generic;

namespace Domain
{
    public class PersonRepository : IPersonRepository
    {
        public IEnumerable<Person> Persons { get; set; }

        public PersonRepository()
        {
            Persons = new List<Person>()
            {
                new Person()
                {
                    Name = "Łukasz",
                    SubName = "Gawin",
                    Age = 23
                }
            };
        }

        public void Add(Person person)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Person person)
        {
            throw new System.NotImplementedException();
        }
    }
}