﻿using Conventions.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void CreatePerson(Person person)
        {
            _people.Add(person);
        }

        public void UpdatePerson(Person person)
        {
            var index = _people.FindIndex(existingPerson => existingPerson.Id == person.Id);
            _people[index] = person;
        }

        public void DeletePerson(Guid id)
        {
            var index = _people.FindIndex(existingPerson => existingPerson.Id == id);
            _people.RemoveAt(index);
        }
    }
}
