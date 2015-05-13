using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMonitor.Common.Models;
using Xunit;

namespace BMonitor.Common.Models
{
    public class RangeUtilTests
    {
        [Fact()]
        public void Range_Contains_10_inside_low_20()
        {
            Range r = new Range(20, 20);

            Assert.True(r.Contains(10d), "not implemented yet");
        }
    }
}
