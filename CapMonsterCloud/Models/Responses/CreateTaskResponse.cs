namespace CapMonsterCloud.Models.Responses
{
    internal class CreateTaskResponse : BaseResponse
    {
        /// <summary>
        /// Task Id for future use in getTaskResult method.
        /// </summary>
        public int TaskId { get; set; }
    }
}
