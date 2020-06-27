using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterCloud.Models.CaptchaTasksResults
{
    public class ImageToTextTaskResult : CaptchaTaskResult
    {
        /// <summary>
        /// Captcha answer
        /// </summary>
        public string Text { get; set; }
    }
}
