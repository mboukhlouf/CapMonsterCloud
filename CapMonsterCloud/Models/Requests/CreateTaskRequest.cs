using CapMonsterCloud.Models.CaptchaTasks;

namespace CapMonsterCloud.Models.Requests
{
    internal class CreateTaskRequest : BaseRequest
    {
        /// <summary>
        /// Task data
        /// </summary>
        public CaptchaTask Task { get; set; }
    }
}
