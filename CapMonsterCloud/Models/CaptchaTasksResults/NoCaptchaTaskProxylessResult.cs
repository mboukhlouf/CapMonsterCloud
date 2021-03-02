namespace CapMonsterCloud.Models.CaptchaTasksResults
{
    public class NoCaptchaTaskProxylessResult : CaptchaTaskResult
    {
        /// <summary>
        /// Recaptcha solution
        /// </summary>
        public string GRecaptchaResponse { get; set; }
    }
}
