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
