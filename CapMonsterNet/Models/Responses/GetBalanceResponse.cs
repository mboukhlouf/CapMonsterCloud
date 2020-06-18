using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterNet.Models.Responses
{
    public class GetBalanceResponse : BaseResponse
    {
        /// <summary>
        /// Available balance
        /// </summary>
        public decimal Balance { get; set; }
    }
}
