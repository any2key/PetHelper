using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using PetHelper.Models.Buisness;
using WebKeyGenerator.Models.Buisness;
using WebKeyGenerator.Models.Identity;
using WebKeyGenerator.Models.Requests;
using WebKeyGenerator.Models.Responses;
using WebKeyGenerator.Services;

namespace WebKeyGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController: ControllerBaseEx
    {

        private readonly IUserService userService;
        private readonly ITokenService tokenService;
        private readonly IDataService dataService;
        private readonly IConfiguration config;
        private readonly ILogger<AdminController> _logger;


        string zipFolder;
        string rootpath;

        public AdminController(IUserService userService, ITokenService tokenService, IDataService dataService, IConfiguration config, IWebHostEnvironment env)
        {
            this.userService = userService;
            this.tokenService = tokenService;
            this.dataService = dataService;
            this.config = config;

            zipFolder= Path.Combine(env.ContentRootPath, "zips");
            rootpath = env.ContentRootPath;
        }

        [HttpGet]
       [Authorize(Roles = "admin")]
        [Route("fetchusers")]
        public async Task<IActionResult> FetchUsers()
        {
            return SafeRun(_ =>
            {
                logger.Trace($"User id {UserID}");
                WebKeyGenerator.Utils.Logger log = new Utils.Logger();
                var res = dataService.Fetch();
                return new DataResponse<IEnumerable<User>>() {Data=res,IsOk=true };
            });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("AddOrUpdateUser")]
        public async Task<IActionResult> AddOrUpdateUser(User user)
        {
            logger.Trace($"User id {UserID}");

            return SafeRun(_ =>
            {
                dataService.AddOrUpdateUser(user);
                return WebKeyGenerator.Models.Responses.Response.OK;
            });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("getuser")]
        public async Task<IActionResult> Getuser(int userId)
        {
            logger.Trace($"User id {UserID}");

            return SafeRun(_ =>
            {
                var res = dataService.GetUser(userId);
                return new DataResponse<User>() { Data = res, IsOk = true };
            });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("deleteuser")]
        public async Task<IActionResult> Deleteuser(int userId)
        {
            logger.Trace($"User id {UserID}");

            return SafeRun(_ =>
            {
                dataService.RemoveUser(userId);
                return WebKeyGenerator.Models.Responses.Response.OK;

            });
        }

        [HttpGet]
       // [Authorize(Roles = "admin")]
        [Route("fetchspec")]
        public async Task<IActionResult> FetchSpec()
        {
            return SafeRun(_ =>
            {

                return new DataResponse<IEnumerable<Specialty>>() { Data = dataService.Specialties(), IsOk = true };

            });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("addspec")]
        public async Task<IActionResult> AddSpec([FromBody] Specialty spec)
        {
            return SafeRun(_ =>
            {

                dataService.AddSpeciality(spec);
                return WebKeyGenerator.Models.Responses.Response.OK;

            });
        }


        [HttpGet]
        [Route("reqscount")]
        public async Task<IActionResult> ReqsCount()
        {
            return SafeRun(_ => 
            {
                return new DataResponse<int>() {IsOk=true,Data= dataService.ReqsCount() };
            });
        }

        [HttpGet]
        [Route("reqs")]
        public async Task<IActionResult> Reqs()
        {
            return SafeRun(_ => 
            {
                return new DataResponse<IEnumerable<Doctor>>() {IsOk=true,Data=dataService.Requests() };
            });
        }

        [HttpGet]
        [Route("activatereq")]
        public async Task<IActionResult> activatereq(int id)
        {
            return SafeRun(_ =>
            {
                dataService.ActivateRequest(id,config);
                return WebKeyGenerator.Models.Responses.Response.OK;
            });
        }


        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> Download(int id) 
        {
            var mimeType = "application/zip";

            return new FileStreamResult(dataService.GetFiles(id), mimeType)
            {
                FileDownloadName = "data.zip"
            };

        }

    }
}
