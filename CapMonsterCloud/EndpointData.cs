using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace CapMonsterCloud
{
    internal class EndpointData
    {
        public Uri Uri { get; set; }

        public HttpMethod Method { get; set; }

        public EndpointSecurityType SecurityType { get; set; }

        public EndpointData(Uri uri)
        {
            Uri = uri;
            Method = HttpMethod.Get;
            SecurityType = EndpointSecurityType.None;
        }

        public EndpointData(Uri uri, HttpMethod method, EndpointSecurityType securityType = EndpointSecurityType.None)
        {
            Uri = uri;
            Method = method;
            SecurityType = securityType;
        }

        public override string ToString()
        {
            return Uri.ToString();
        }
    }
}
