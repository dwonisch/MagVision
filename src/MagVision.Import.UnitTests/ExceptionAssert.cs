using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagVision.Import.UnitTests
{
    public static class ExceptionAssert
    {
        public static void Throws<T>(Action task) where T : Exception
        {
            try
            {
                task();
            }
            catch (Exception ex)
            {
                if (!typeof(T).IsInstanceOfType(ex))
                    Assert.Fail("Expected exception of Type '{0}' but was '{1}'", typeof(T).Name, ex.GetType().Name);
                return;
            }
            
            Assert.Fail("Expected exception but no exception was thrown.");
        }
    }
}
