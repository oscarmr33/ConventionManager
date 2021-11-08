using Conventions.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conventions.API.Repositories.Interfaces
{
    public interface ITalksRepository
    {
        IEnumerable<Talk> GetTalks();
        Talk GetTalk(Guid id);
        void CreateTalk(Talk talk);
        void UpdateTalk(Talk talk);
        void DeleteTalk(Guid id);
    }
}
