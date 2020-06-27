using System;
using System.Collections.Generic;
using System.Text;

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
