namespace CapMonsterCloud.Models.CaptchaTasksResults
{
    public class NoCaptchaTaskResult : CaptchaTaskResult
    {
        /// <summary>
        /// Recaptcha solution
        /// </summary>
        public string GRecaptchaResponse { get; set; }
    }
}
