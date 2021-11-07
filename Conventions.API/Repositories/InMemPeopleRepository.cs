using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conventions.API.Repositories
{
    public class InMemPeopleRepository : IPeopleRepository
    {
        private readonly List<Person> _people = new()
        {
            new Person { Id = Guid.Parse("7b80daf0-3cca-446a-bb21-f8036761d115"), FirstName = "Oscar", LastName = "Medina", Email = "oscarmedinaici@hotmail.com", Telephone = "52810373" },
            new Person { Id = Guid.Parse("0e2118b1-ba06-4145-a42b-51dd710c44d0"), FirstName = "Maj", LastName = "Abildgren", Email = "majabildgren@gmail.com", Telephone = "52541640" },
            new Person { Id = Guid.Parse("74155be9-fd2a-4d34-8cc1-b82169d7d97b"), FirstName = "Raul", LastName = "Pineda", Email = "hola@raulpineda.com", Telephone = "56987423" }
        };

        public IEnumerable<Person> GetPeople()
        {
            return _people;
        }

        public Person GetPerson(Guid id)
        {
            return _people.SingleOrDefault(person => person.Id == id);
        }
    }
}
