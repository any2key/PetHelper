using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebKeyGenerator.Context;
using WebKeyGenerator.Utils;

namespace WebKeyGenerator.Controllers
{
    public class ControllerBaseEx : ControllerBase
    {



        protected int? UserID => int.Parse(FindClaim(ClaimTypes.NameIdentifier));
        protected string UserRole => FindClaim(ClaimTypes.Role);
        public WebKeyGenerator.Utils.Logger logger = new Utils.Logger();
        private string FindClaim(string claimName)
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(claimName);
            if (claim == null)
            {
                return null;
            }
            return claim.Value;
        }

        protected IActionResult SafeRun(Func<string, object> action)
        {

            logger.Trace($"[{Request.Method}]. {Request.GetEncodedUrl()}");

            try
            {
                var resp = action("");
                logger.Trace($"Response: {resp.ToJson()}");
                return new JsonResult(resp);
            }
            catch (CodeException ex)
            {
                logger.Error(ex.Message);
                return Unauthorized("invalid_grant");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return new JsonResult(WebKeyGenerator.Models.Responses.Response.BadResponse(ex.Message));
            }
        }

        public class CodeException : Exception
        {
            public CodeException()
            {

            }
            public CodeException(string message)
                : base(message)
            {
            }
        }
    }
}
