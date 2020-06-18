using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CapMonsterNet.Models.Responses
{
    public abstract class BaseResponse
    {
        public int ErrorId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ErrorType? ErrorCode { get; set; }

        [JsonIgnore]
        public string ErrorDescription
        {
            get
            {
                if (!ErrorCode.HasValue)
                    return null;
                return ErrorCode.Value.GetDescription();
            }
        }
    }
}
