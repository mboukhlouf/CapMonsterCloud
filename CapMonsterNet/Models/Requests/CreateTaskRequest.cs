using CapMonsterNet.Models.CaptchaTasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterNet.Models.Requests
{
    public class CreateTaskRequest : BaseRequest
    {
        /// <summary>
        /// Task data
        /// </summary>
        public CaptchaTask Task { get; set; }
    }
}
