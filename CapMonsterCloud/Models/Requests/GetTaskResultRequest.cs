using CapMonsterCloud.Models.CaptchaTasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterCloud.Models.Requests
{
    internal class GetTaskResultRequest : BaseRequest
    {
        /// <summary>
        /// ID which was obtained in createTask method.
        /// </summary>
        public int TaskId { get; set; }
    }
}
