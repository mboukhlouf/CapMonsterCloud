using CapMonsterNet.Exceptions;
using CapMonsterNet.Models.CaptchaTasks;
using CapMonsterNet.Models.CaptchaTasksResults;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CapMonsterNet.Example
{
    class Program
    {
        private static readonly string Token = "TOKEN";
        private static readonly CapMonsterClient client = new CapMonsterClient(Token);

        static void Main(string[] args)
        {
            TestRecaptchaAsync().GetAwaiter().GetResult();
        }

        private static async Task TestRecaptchaAsync()
        {
            var captchaTask = new NoCaptchaTaskProxyless
            {
                WebsiteUrl = "URL",
                WebsiteKey = "KEY",
                UserAgent = "USER_AGENT"
            };

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int taskId;
            try
            {
                taskId = await client.CreateTaskAsync(captchaTask);
            }
            catch (CapMonsterException e)
            {
                Console.WriteLine($"{e.ErrorCode}: {e.ErrorDescription}");
                return;
            }

            try
            {
                var solution = await client.GetTaskResultAsync<NoCaptchaTaskProxylessResult>(taskId);
                stopwatch.Stop();
                Console.WriteLine($"Solved in {stopwatch.ElapsedMilliseconds}ms.");
            }
            catch (CapMonsterException e)
            {
                Console.WriteLine($"{e.ErrorCode}: {e.ErrorDescription}");
                return;
            }
        }
    }
}
