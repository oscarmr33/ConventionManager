using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conventions.API.Repositories
{
    public interface IPeopleRepository
    {
        IEnumerable<Person> GetPeople();
        Person GetPerson(Guid id);
        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Guid id);
    }
}
