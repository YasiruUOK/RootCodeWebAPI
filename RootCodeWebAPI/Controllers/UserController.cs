using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RootCodeWebAPI.Models;
using RootCodeWebAPI.VM;

namespace RootCodeWebAPI.Controllers
{
    [RoutePrefix("Api/user")]
    public class UserController : ApiController
    {
        RootCodeTestDBEntities DB = new RootCodeTestDBEntities();
        
        [Route("GetWritersList")]
        [HttpGet]
        public  async Task<List<UserInfo>> getWritersListAsync()
        {
            List<Post> posts = await DB.Posts.ToListAsync();
            List<int> writersIDList = new List<int>();
            List<UserInfo> userInfos = new List<UserInfo>();
            for (int i=0;i<posts.Count;i++)
            {
                if (!writersIDList.Contains((int)posts[i].UserId))
                {
                    writersIDList.Add((int)posts[i].UserId);
                    int userID = (int)posts[i].UserId;
                    var writerDetails= DB.UserInfoes.Where(x => x.UserId== userID).FirstOrDefault();
                    userInfos.Add(writerDetails);
                }
            }
            return userInfos;
        }
    }
}