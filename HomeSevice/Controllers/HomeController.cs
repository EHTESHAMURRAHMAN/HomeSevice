using Azure;
using HomeSevice.Model;
using Microsoft.AspNetCore.Mvc;
using static HomeSevice.Model.Authenticate;

namespace HomeSevice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [HttpPost]
        [Route("createAccount")]
        public RResponse CreateAccount([FromBody] Authenticate authenticate)
        {
            Authenticate auth = new Authenticate();
            var result = auth.CreateAccount(authenticate);
            return result;
        }
        [HttpGet]
        [Route("getprofile/{userid}")]
        public ProfileResponse get_userdetail(int userid)
        {
            ProfileResponse profile = new ProfileResponse();
            profile = Authenticate.GetProfileDetail(userid);
            return profile;
        }
        [HttpPost]
        [Route("updateprofile")]
        public Result UpdateProfile([FromBody] Authenticate objInsertKey)
        {
            Authenticate obj = new Authenticate();
            var result = obj.UpdateProfile(objInsertKey);
            return result;
        }
        [HttpGet]
        [Route("Deleteprofile/{userid}")]
        public Result? Delete_userdetail(int userid)
        {
            var result = Authenticate.DeleteProfileDetail(userid);
            return result;
        }
    }
}
