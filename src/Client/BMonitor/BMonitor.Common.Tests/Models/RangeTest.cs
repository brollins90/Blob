using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BMonitor.Common.Models
{
    public class RangeTests
    {
        // http://osherove.com/blog/2005/4/3/naming-standards-for-unit-tests.html
        [Fact()]
        public void RangeContains_10OutsideLow20_True()
        {
            Range r = new Range(20d, double.PositiveInfinity);

            Assert.True(r.Contains(10), "not implemented yet");
        }

        [Fact()]
        public void RangeContains_20OutsideLow10_False()
        {
            Range r = new Range(10d, double.PositiveInfinity);

            Assert.False(r.Contains(20), "not implemented yet");
        }
        [Fact()]
        public void RangeContains_10InsideLow20_True()
        {
            Range r = new Range(20d, double.PositiveInfinity, true);

            Assert.True(r.Contains(10), "not implemented yet");
        }

        [Fact()]
        public void RangeContains_20InsideLow10_False()
        {
            Range r = new Range(10d, double.PositiveInfinity, true);

            Assert.False(r.Contains(20), "not implemented yet");
        }
    }
}
