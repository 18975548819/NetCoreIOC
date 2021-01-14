using ITestHepler;
using System;

namespace TestHepler
{
    public class CustomTest : ICustomTest
    {
        public string GetDateTime()
        {
            return DateTime.Now.ToString();
        }
    }
}
