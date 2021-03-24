using DistributedLibrary.Authors.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DistributedLibrary.OtherAuthors.APi.Services
{
    public class OtherService : IOtherService
    {
        private const string FileName = @"authors.json";
        public IEnumerable<Autores> GetEntities()
        {
            return JsonConvert.DeserializeObject<IEnumerable<Autores>>(File.ReadAllText(FileName));
        }

        public Autores GetEntityById(int entityId)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Autores>>(File.ReadAllText(FileName)).Where(x => x.Id == entityId).FirstOrDefault();
        }
    }
}
