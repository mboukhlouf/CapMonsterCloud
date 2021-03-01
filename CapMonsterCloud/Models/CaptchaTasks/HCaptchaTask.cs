using Newtonsoft.Json;

namespace CapMonsterCloud.Models.CaptchaTasks
{
    public class HCaptchaTask : CaptchaTask
    {
        public HCaptchaTask()
        {
            Type = "HCaptchaTask";
        }

        /// <summary>
        /// Address of a webpage with HCaptcha
        /// </summary>
        [JsonProperty("websiteURL")]
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// HCaptcha website key.
        /// </summary>
        public string WebsiteKey { get; set; }

        /// <summary>
        /// Type of the proxy
        /// http, https, socks4, socks5
        /// </summary>
        public string ProxyType { get; set; }

        /// <summary>
        /// Proxy IP address IPv4/IPv6
        /// </summary>
        public string ProxyAddress { get; set; }

        /// <summary>
        /// Proxy port
        /// </summary>
        public int ProxyPort { get; set; }

        /// <summary>
        /// Login for proxy which requires authorizaiton (basic)
        /// </summary>
        public string ProxyUsername { get; set; }

        /// <summary>
        /// Proxy password
        /// </summary>
        public string ProxyPassword { get; set; }

        /// <summary>
        /// Browser's User-Agent which is used in emulation. 
        /// It is required that you use a signature of a modern browser, 
        /// otherwise HCaptcha will ask you to "update your browser".
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Additional cookies which we must use during interaction with target page or HCaptcha.
        /// Format: cookiename1=cookievalue1; cookiename2=cookievalue2
        /// </summary>
        public string Cookies { get; set; }
    }
}
