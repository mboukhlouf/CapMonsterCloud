namespace CapMonsterCloud.Models.Responses
{
    internal class GetBalanceResponse : BaseResponse
    {
        /// <summary>
        /// Available balance
        /// </summary>
        public decimal Balance { get; set; }
    }
}
