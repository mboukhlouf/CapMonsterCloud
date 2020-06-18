using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterNet.Models.CaptchaTasks
{
    public class RecaptchaV3TaskProxyless : CaptchaTask
    {
        public RecaptchaV3TaskProxyless()
        {
            Type = "RecaptchaV3TaskProxyless";
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
        /// Minimum score, value from 0.1 to 0.9.
        /// </summary>
        public double MinScore { get; set; }

        /// <summary>
        /// Widget action value. Website owner defines what user is doing on the page through this parameter. 
        /// Default value: verify
        /// Example:
        /// grecaptcha.execute('site_key', {action:'login_test'}).
        /// </summary>
        public string PageAction { get; set; }
    }
}
