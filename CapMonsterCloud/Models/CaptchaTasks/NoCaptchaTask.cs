using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterCloud.Models.CaptchaTasks
{
    public class NoCaptchaTask : CaptchaTask
    {
        public NoCaptchaTask()
        {
            Type = "NoCaptchaTask";
        }

        /// <summary>
        /// Address of a webpage with Google ReCaptcha
        /// </summary>
        [JsonProperty("websiteURL")]
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// Recaptcha website key.
        /// </summary>
        public string WebsiteKey { get; set; }

        /// <summary>
        /// Some custom implementations may contain additional "data-s" parameter in ReCaptcha2 div, 
        /// which is in fact a one-time token and must be grabbed every time you want to solve a ReCaptcha2.
        /// </summary>  
        public string RecaptchaDataSValue { get; set; }

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
        /// otherwise Google will ask you to "update your browser".
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Additional cookies which we must use during interaction with target page or Google.
        /// Format: cookiename1=cookievalue1; cookiename2=cookievalue2
        /// </summary>
        public string Cookies { get; set; }
    }
}
