using Freed.FrameWork.AttributeHepler;
using System;

namespace ITestHepler
{
    public interface ICustomTest
    {
        [LogBefore]
        [AfterIntercetor]
        string GetDateTime();
    }
}
