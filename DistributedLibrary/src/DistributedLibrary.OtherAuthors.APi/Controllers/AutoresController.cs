using DistributedLibrary.OtherAuthors.APi.Models;
using DistributedLibrary.OtherAuthors.APi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistributedLibrary.OtherAuthors.APi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutoresController : ControllerBase
    {

        private readonly IOtherService _dataService;
      

        public AutoresController( IOtherService dataService)
        {

            _dataService = dataService;
            
        }

        [HttpGet("{Id}")]
        public ActionResult<IEnumerable<OtrosAutores>> GetEntityById(int Id)
        {

            var result = _dataService.GetEntityById(Id);
            return Ok(result);

        }



       
    }
   
}
