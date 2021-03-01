namespace CapMonsterCloud.Models.CaptchaTasks
{
    public abstract class CaptchaTask
    {
        /// <summary>
        /// Defines the type of the task.
        /// </summary>
        public string Type { get; protected set; }

        /// <summary>
        /// Threshold of system confidence in response
        /// </summary>
        public int? RecognizingThreshold { get; set; }
    }
}
