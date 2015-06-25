using Xunit;

namespace BMonitor.Common.Operations
{
    public class GreaterThanTests
    {
        private GreaterThan sut = new GreaterThan();

        [Fact]
        public void GreaterThanDoubles_ActualEqual_LimitNotBroken()
        {
            double limit = 1d;
            double actual = 1d;

            Assert.False(sut.LimitBroken(limit, actual));
        }

        [Fact]
        public void GreaterThanDoubles_ActualGreater_LimitBroken()
        {
            double limit = 1d;
            double actual = 10d;

            Assert.True(sut.LimitBroken(limit, actual));
        }

        [Fact]
        public void GreaterThanDoubles_ActualLess_LimitNotBroken()
        {
            double limit = 10d;
            double actual = 1d;

            Assert.False(sut.LimitBroken(limit, actual));
        }
    }
}
