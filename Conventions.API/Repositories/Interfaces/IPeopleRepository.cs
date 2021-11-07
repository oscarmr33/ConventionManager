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
    }
}
