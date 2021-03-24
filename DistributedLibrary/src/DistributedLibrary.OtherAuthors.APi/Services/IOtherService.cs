using DistributedLibrary.OtherAuthors.APi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistributedLibrary.OtherAuthors.APi.Services
{
    public interface IOtherService
    {
        IEnumerable<OtrosAutores> GetEntities();

        OtrosAutores GetEntityById(int entityId);
    }
}
