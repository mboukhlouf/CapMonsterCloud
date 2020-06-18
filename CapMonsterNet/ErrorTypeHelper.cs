using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterNet
{
    public static class ErrorTypeHelper
    {
        private static readonly Dictionary<ErrorType, string> descriptions = new Dictionary<ErrorType, string>()
        {
            { ErrorType.ERROR_KEY_DOES_NOT_EXIST, "Account authorization key not found in the system or has incorrect format (length is not )" },
            { ErrorType.ERROR_ZERO_CAPTCHA_FILESIZE, "The size of the captcha you are uploading is less than 100 bytes." },
            { ErrorType.ERROR_TOO_BIG_CAPTCHA_FILESIZE, "The size of the captcha you are uploading is more than 50,000 bytes." },
            { ErrorType.ERROR_ZERO_BALANCE, "Account has zero balance" },
            { ErrorType.ERROR_IP_NOT_ALLOWED, "Request with current account key is not allowed from your IP" },
            { ErrorType.ERROR_CAPTCHA_UNSOLVABLE, "This type of captchas is not supported by the service or the image does not contain an answer, perhaps it is too noisy. It could also mean that the image is corrupted or was incorrectly rendered." },
            { ErrorType.ERROR_NO_SUCH_CAPCHA_ID, "The captcha that you are requesting was not found. Make sure you are requesting a status update only within 5 minutes of uploading." },
            { ErrorType.WRONG_CAPTCHA_ID, "The captcha that you are requesting was not found. Make sure you are requesting a status update only within 5 minutes of uploading." },
            { ErrorType.CAPTCHA_NOT_READY, "The captcha has not yet been solved" },
            { ErrorType.ERROR_IP_BANNED, "You have exceeded the limit of requests with the wrong api key, check the correctness of your api key in the control panel and after some time, try again" },
            { ErrorType.ERROR_NO_SUCH_METHOD, "This method is not supported or empty" },
        };

        public static string GetDescription(this ErrorType errorType)
        {
            return descriptions[errorType];
        }
    }
}
