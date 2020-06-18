using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterNet.Models.CaptchaTasksResults
{
    public class FunCaptchaTaskProxylessResult : CaptchaTaskResult
    {
        /// <summary>
        /// FunCaptcha token that needs to be substituted into the form.
        /// </summary>
        public string Token { get; set; }
    }
}
