using Conventions.Models.Entities;
using System;
using System.Collections.Generic;

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
