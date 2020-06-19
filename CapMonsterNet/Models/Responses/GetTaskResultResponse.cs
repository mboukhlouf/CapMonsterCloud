using CapMonsterNet.Models.CaptchaTasksResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterNet.Models.Responses
{
    internal class GetTaskResultResponse : BaseResponse
    {
        /// <summary>
        /// processing - task is not ready yet
        /// ready - task complete, solution object can be found in solution property
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Task result data
        /// </summary>
        public CaptchaTaskResult Solution { get; set; }
    }
}
