using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterNet.Models.Requests
{
    public abstract class BaseRequest
    {
        /// <summary>
        /// Client account key
        /// </summary>
        public string ClientKey { get; set; }
    }
}
