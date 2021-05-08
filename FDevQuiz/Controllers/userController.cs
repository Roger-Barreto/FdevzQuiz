using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDevQuiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        public ActionResult getUser()
        {
            var user = JsonConvert.DeserializeObject(System.IO.File.ReadAllText(@"c:\JsonData\user.json"));

            return Ok(user);
        }
    }
}
