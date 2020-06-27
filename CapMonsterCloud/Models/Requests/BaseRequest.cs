using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterCloud.Models.Requests
{
    internal abstract class BaseRequest
    {
        /// <summary>
        /// Client account key
        /// </summary>
        public string ClientKey { get; set; }
    }
}
