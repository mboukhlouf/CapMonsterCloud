using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterCloud.Models.CaptchaTasksResults
{
    public class NoCaptchaTaskResult : CaptchaTaskResult
    {
        /// <summary>
        /// Recaptcha solution
        /// </summary>
        public string GRecaptchaResponse { get; set; }
    }
}
