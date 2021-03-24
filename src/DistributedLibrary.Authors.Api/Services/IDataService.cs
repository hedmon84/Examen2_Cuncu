using DistributedLibrary.Authors.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DistributedLibrary.Authors.Api.Services
{
    public interface IDataService
    {
        IEnumerable<Autores> GetEntities();

        Autores GetEntityById(int entityId);
    }
}
