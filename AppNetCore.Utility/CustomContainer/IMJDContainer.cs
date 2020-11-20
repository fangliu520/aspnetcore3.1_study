using System;
using System.Collections.Generic;
using System.Text;

namespace AppNetCore.Utility.CustomContainer
{
    public interface IMJDContainer
    {
        void Register<TFrom, TTo>(string shortName = null, object[] paraList = null, LifetimeType lifetimeType = LifetimeType.Transient) where TTo : TFrom;

        TFrom Resolve<TFrom>(string shortName = null);
        IMJDContainer CreateChildContainer();
    }
}
