using System;
using System.Collections.Generic;
using unityPresenting.Core;

namespace unityPresenting.Test
{
    public class MockMonoViewProvider : IViewResolver
    {
        public TView Resolve<TView>(string key)
        {
            throw new NotImplementedException();
        }

        public List<ViewData> ReadData(List<ViewData> presenterData)
        {
            throw new NotImplementedException();
        }
    }
}