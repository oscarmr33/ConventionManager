using Conventions.API.Entities;
using Conventions.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conventions.API.Repositories
{
    public class InMenTalksRepository : ITalksRepository
    {
        private readonly List<Talk> _talks = new()
        {
            //new Talk()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "First talk Spring",
            //    Convention 
            //}
        };

        public IEnumerable<Talk> GetTalks()
        {
            throw new NotImplementedException();
        }
        
        public Talk GetTalk(Guid id)
        {
            throw new NotImplementedException();
        }       
    }
}
