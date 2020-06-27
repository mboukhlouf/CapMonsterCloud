using System;
using System.Collections.Generic;
using System.Text;

namespace CapMonsterCloud
{
    public enum ErrorType
    {
        ERROR_KEY_DOES_NOT_EXIST,
        ERROR_ZERO_CAPTCHA_FILESIZE,
        ERROR_TOO_BIG_CAPTCHA_FILESIZE,
        ERROR_ZERO_BALANCE,
        ERROR_IP_NOT_ALLOWED,
        ERROR_CAPTCHA_UNSOLVABLE,
        ERROR_NO_SUCH_CAPCHA_ID,
        WRONG_CAPTCHA_ID,
        CAPTCHA_NOT_READY,
        ERROR_IP_BANNED,
        ERROR_NO_SUCH_METHOD
    }
}
