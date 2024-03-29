using Microsoft.AspNetCore.Mvc;
using Shared.dto.api;
using Shared.interfaces;
using TgimbaNetCoreWebShared;
using TgimbaNetCoreWebShared.Controllers;

namespace TgimbaNetCoreWeb.Controllers
{
//#if !DEBUG
//    [RequireHttpsAttribute]
//#endif
    [Route("api/[controller]")]
    [ApiController]
    public class TgimbaApiController : ControllerBase
    {
        private SharedTgimbaApiController sharedTgimbaApiController = null;

        public TgimbaApiController(ITgimbaService service, IValidationHelper validationHelper)
        {
            this.sharedTgimbaApiController = new SharedTgimbaApiController(service, validationHelper);
        }

        #region User

        [HttpPost("processuserregistration")]
        public IActionResult ProcessUserRegistration([FromBody] RegistrationRequest request)
        {
            return this.sharedTgimbaApiController.ProcessUserRegistration(request);
        }

        [HttpPost("processuser")]
        public IActionResult ProcessUser([FromBody] LoginRequest request)
        {
            return this.sharedTgimbaApiController.ProcessUser(request);
        }

        [HttpDelete("deleteuserbucketlistitems/{userName:alpha}/{onlyDeleteBucketListItems:alpha}")]
        public IActionResult DeleteUserBucketListItems(string userName, string onlyDeleteBucketListItems)
        {
            var deleteBucketListItems = System.Convert.ToBoolean(onlyDeleteBucketListItems);
            return this.sharedTgimbaApiController.DeleteUserBucketListItems(
                 Utilities.GetHeaderValue("EncodedJwtPrivateKey", this.Request),
                 userName,
                 deleteBucketListItems);
        }

        #endregion

        #region Bucket List Items

        [HttpDelete("delete/{BucketListItemId:int}")]
        public IActionResult DeleteBucketListItem(int BucketListItemId)
        {
            return this.sharedTgimbaApiController.DeleteBucketListItem
            (
                Utilities.GetHeaderValue("EncodedUserName", this.Request),
                Utilities.GetHeaderValue("EncodedToken", this.Request), 
                BucketListItemId
             );
        }

        [HttpGet("getbucketlistitems")]
        public IActionResult GetBucketListItem([FromQuery] GetBucketListItemRequest request)
        {
            return this.sharedTgimbaApiController.GetBucketListItem(request);
        }

        [HttpPost("upsert")]
        public IActionResult UpsertBucketListItem([FromBody] UpsertBucketListItemRequest request)
        {
            return this.sharedTgimbaApiController.UpsertBucketListItem(request);
        }

        #endregion

        #region Misc

        [HttpGet("getsystemstatistics")]
        public IActionResult GetSystemStatistics()
        {
            System.Console.WriteLine("TgimbaApi-GetSystemStatistics()");

            var result = this.sharedTgimbaApiController.GetSystemStatistics
            (
                Utilities.GetHeaderValue("EncodedUserName", this.Request),
                Utilities.GetHeaderValue("EncodedToken", this.Request)
            );

            System.Console.WriteLine("TgimbaApi-GetSystemStatistics() - result {0}", result);

            return result;
        }

        [HttpGet("getsystembuildstatistics")]
        public IActionResult GetSystemBuildStatistics(string encodedUser, string encodedToken)
        {
            return this.sharedTgimbaApiController.GetSystemBuildStatistics
            (
                Utilities.GetHeaderValue("EncodedUserName", this.Request),
                Utilities.GetHeaderValue("EncodedToken", this.Request)
            );
        }

        [HttpPost("log")]
        public IActionResult Log([FromBody] LogMessageRequest request)
        {
            return this.sharedTgimbaApiController.Log(request);
        }

        [HttpGet("test")]
        public IActionResult GetTestResult()
        {
            return this.sharedTgimbaApiController.GetTestResult();
        }

        #endregion
    }
}
