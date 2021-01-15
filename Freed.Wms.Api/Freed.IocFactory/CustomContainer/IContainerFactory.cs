using System;
using System.Collections.Generic;
using System.Text;

namespace Freed.IocFactory.CustomContainer
{
    /// <summary>
    /// 定义抽象接口
    /// </summary>
    public interface IContainerFactory
    {
        void Register<TFronm, TTo>() where TTo : TFronm; //泛型：代表TTo是来自TFronm
        TFronm Resolve<TFronm>();                                                           
    }
}
