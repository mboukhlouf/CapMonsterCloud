using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterCloud.Models.CaptchaTasks
{
    public class FunCaptchaTaskProxyless : CaptchaTask
    {
        public FunCaptchaTaskProxyless()
        {
            Type = "FunCaptchaTaskProxyless";
        }

        /// <summary>
        /// Address of a webpage with Google ReCaptcha
        /// </summary>
        [JsonProperty("websiteURL")]
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// A special subdomain of funcaptcha.com, from which the JS captcha widget should be loaded.
        /// Most FunCaptcha installations work from shared domains, so this option is only needed in certain rare cases.
        /// </summary>
        [JsonProperty("funcaptchaApiJSSubdomain")]
        public string FunCaptchaApiJsSubdomain { get; set; }

        /// <summary>
        /// FunCaptcha website key.
        /// <div id="funcaptcha" data-pkey="THAT_ONE"></div>
        /// </summary>
        public double WebsitePublicKey { get; set; }

        /// <summary>
        /// Additional parameter that may be required by Funcaptcha implementation.
        /// Use this property to send "blob" value as a stringified array.See example how it may look like.
        /// {"\blob\":\"HERE_COMES_THE_blob_VALUE\"}
        /// </summary>
        public string Data { get; set; }
    }
}
