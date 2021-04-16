using System.Web.Http;

namespace SD.FileSystem.AppService.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public string Index()
        {
            return "Hello World";
        }
    }
}