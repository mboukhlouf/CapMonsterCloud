using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CapMonsterCloud
{
    internal static class Endpoints
    {
        private static string ApiBaseUrl { get; } = "https://api.capmonster.cloud";

        private static string ApiPrefix { get; } = $"{ApiBaseUrl}";

        public static EndpointData CreateTask()
        {
            return new EndpointData(new Uri($"{ApiPrefix}/createTask"), HttpMethod.Post, EndpointSecurityType.ApiKey);
        }

        public static EndpointData GetTaskResult()
        {
            return new EndpointData(new Uri($"{ApiPrefix}/getTaskResult"), HttpMethod.Post, EndpointSecurityType.ApiKey);
        }

        public static EndpointData GetBalance()
        {
            return new EndpointData(new Uri($"{ApiPrefix}/getBalance"), HttpMethod.Post, EndpointSecurityType.ApiKey);
        }
    }
}
