using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterCloud.Models.CaptchaTasksResults
{
    public class HCaptchaTaskProxylessResult : CaptchaTaskResult
    {
        /// <summary>
        /// Hcaptcha solution
        /// </summary>
        public string GRecaptchaResponse { get; set; }
    }
}
