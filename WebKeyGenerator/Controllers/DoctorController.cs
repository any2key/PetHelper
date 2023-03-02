using WebKeyGenerator.Controllers;
using WebKeyGenerator.Services;
using Microsoft.AspNetCore.Mvc;
using PetHelper.Models.Buisness;
using Microsoft.AspNetCore.Authorization;
using WebKeyGenerator.Models.Responses;

namespace PetHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBaseEx
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;
        private readonly IDataService dataService;
        private readonly ILogger<DoctorController> _logger;
        IConfiguration config;
        public DoctorController(IUserService userService, ITokenService tokenService, IDataService dataService, IConfiguration config)
        {
            this.userService = userService;
            this.tokenService = tokenService;
            this.dataService = dataService;
            this.config = config;
        }


        [HttpPost]
        [Route("createreq")]
        public async Task<IActionResult> CreateReq(DoctorRequest req)
        {
            return SafeRun(_ =>
            {
                dataService.CreateRequest(req,config);
                return WebKeyGenerator.Models.Responses.Response.OK;
            });
        }


        [HttpGet]
        [Route("getconfirm")]
        [Authorize]
        public async Task<IActionResult> GetConfirm()
        {
            return SafeRun(_ =>
            {
                return new DataResponse<bool>() { IsOk=true, Data=dataService.GetConfirm((int)UserID)};
            });
        }
    }
}
