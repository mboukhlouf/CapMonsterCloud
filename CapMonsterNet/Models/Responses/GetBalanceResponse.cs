using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterNet.Models.Responses
{
    internal class GetBalanceResponse : BaseResponse
    {
        /// <summary>
        /// Available balance
        /// </summary>
        public decimal Balance { get; set; }
    }
}
