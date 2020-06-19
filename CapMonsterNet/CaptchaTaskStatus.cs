using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterNet
{
    internal static class CaptchaTaskStatus
    {
        public static string Processing { get; } = "processing";

        public static string Ready { get; } = "ready";
    }
}
