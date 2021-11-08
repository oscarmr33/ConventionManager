using Conventions.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conventions.API.Repositories.Interfaces
{
    public interface IConventionRepository
    {
        IEnumerable<Convention> GetConventions();
        Convention GetConvention(Guid id);
        void CreateConvention(Convention convention);
        void UpdateConvention(Convention convention);
        void DeleteConvention(Guid id);
    }
}
