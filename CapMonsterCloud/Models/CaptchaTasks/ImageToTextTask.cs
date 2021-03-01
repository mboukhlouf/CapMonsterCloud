using Newtonsoft.Json;

namespace CapMonsterCloud.Models.CaptchaTasks
{
    public class ImageToTextTask : CaptchaTask
    {
        public ImageToTextTask()
        {
            Type = "ImageToTextTask";
        }

        /// <summary>
        /// File body encoded in base64. Make sure to send it without line breaks.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Name of recognizing module, for example, “yandex“. 
        /// </summary>
        [JsonProperty("СapMonsterModule")]
        public string СapMonsterModule { get; set; }    
    }
}
