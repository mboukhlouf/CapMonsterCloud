namespace CapMonsterCloud.Models.Requests
{
    internal class GetTaskResultRequest : BaseRequest
    {
        /// <summary>
        /// ID which was obtained in createTask method.
        /// </summary>
        public int TaskId { get; set; }
    }
}
