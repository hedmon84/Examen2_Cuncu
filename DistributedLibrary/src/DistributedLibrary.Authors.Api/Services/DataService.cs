using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using DistributedLibrary.Authors.Api.Models;

namespace DistributedLibrary.Authors.Api.Services
{
    public class DataService : IDataService
    {
        private const string FileName = @"authors.json";
        public IEnumerable<Autores> GetEntities()
        {
            return JsonConvert.DeserializeObject<IEnumerable<Autores>>(File.ReadAllText(FileName));
        }

        public Autores GetEntityById(int entityId)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Autores>>(File.ReadAllText(FileName)).Where(x => x.Id == entityId ).FirstOrDefault();
        }
    }
}
