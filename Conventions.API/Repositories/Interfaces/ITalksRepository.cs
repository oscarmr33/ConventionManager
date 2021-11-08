using Conventions.Models.Entities;
using System;
using System.Collections.Generic;

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
