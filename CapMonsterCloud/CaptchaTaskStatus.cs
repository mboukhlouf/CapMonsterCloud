using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterCloud
{
    internal static class CaptchaTaskStatus
    {
        public static string Processing { get; } = "processing";

        public static string Ready { get; } = "ready";
    }
}
