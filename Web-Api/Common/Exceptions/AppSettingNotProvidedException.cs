using System;

namespace Web_Api.Common.Exceptions
{
    public class AppSettingNotProvidedException : Exception
    {
        public AppSettingNotProvidedException(string message) : base(message) { }
    }
}
