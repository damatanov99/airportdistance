using System;
using System.Collections.Generic;
using System.Text;

namespace Cteleport.Model
{
    public class Error
    {
        public string error_description { get; set; }
        public ErrorCodes error_code { get; set; }
    }

    public enum ErrorCodes
    {
        None = 0,
        InvalidCode = 1,
        ConnectionError = 2,
        Unknown = 99,
    }
}
