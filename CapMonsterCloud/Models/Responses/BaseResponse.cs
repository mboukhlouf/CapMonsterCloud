using CapMonsterCloud.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CapMonsterCloud.Models.Responses
{
    internal abstract class BaseResponse
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

        /// <summary>
        /// Throws a CapMonsterException in case there was an error in the response
        /// </summary>
        public void EnsureSuccess()
        {
            if(ErrorId == 1)
            {
                throw new CapMonsterException(ErrorId, (ErrorType)ErrorCode, ErrorDescription);
            }
        }
    }
}
