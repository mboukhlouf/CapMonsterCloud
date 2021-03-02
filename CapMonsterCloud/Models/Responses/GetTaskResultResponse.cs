using CapMonsterCloud.Models.CaptchaTasksResults;

namespace CapMonsterCloud.Models.Responses
{
    internal class GetTaskResultResponse<CaptchaTaskResultT> : BaseResponse where CaptchaTaskResultT : CaptchaTaskResult
    {
        /// <summary>
        /// processing - task is not ready yet
        /// ready - task complete, solution object can be found in solution property
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Task result data
        /// </summary>
        public CaptchaTaskResultT Solution { get; set; }
    }
}
