using RootCodeWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RootCodeWebAPI.Controllers
{
    [RoutePrefix("Api/stats")]
    public class StatController : ApiController
    {
        RootCodeTestDBEntities DB = new RootCodeTestDBEntities();

        [Route("GetStatistics")]
        [HttpGet]
        public async Task<List<StatVowel>> getStatistics()
        {
            List<StatVowel> statVowels = await DB.StatVowels.ToListAsync();
            return statVowels;
        }
    }
}