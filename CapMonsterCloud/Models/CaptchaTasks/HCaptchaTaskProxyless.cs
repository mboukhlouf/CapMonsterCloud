using Newtonsoft.Json;

namespace CapMonsterCloud.Models.CaptchaTasks
{
    public class HCaptchaTaskProxyless : CaptchaTask
    {
        public HCaptchaTaskProxyless()
        {
            Type = "HCaptchaTaskProxyless";
        }

        /// <summary>
        /// Address of a webpage with Hcaptcha
        /// </summary>
        [JsonProperty("websiteURL")]
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// HCaptcha website key.
        /// </summary>
        public string WebsiteKey { get; set; }

        /// <summary>
        /// Browser's User-Agent which is used in emulation. 
        /// It is required that you use a signature of a modern browser, 
        /// otherwise Hcaptcha will ask you to "update your browser".
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Additional cookies which we must use during interaction with target page or Hcaptcha.
        /// Format: cookiename1=cookievalue1; cookiename2=cookievalue2
        /// </summary>
        public string Cookies { get; set; }
    }
}