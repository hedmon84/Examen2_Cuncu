using DistributedLibrary.Authors.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistributedLibrary.OtherAuthors.APi.Services
{
    public interface IOtherService
    {
        IEnumerable<Autores> GetEntities();

        Autores GetEntityById(int entityId);
    }
}
