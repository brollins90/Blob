using Xunit;

namespace BMonitor.Common.Operations
{
    public class LessThanTests
    {
        private LessThan sut = new LessThan();

        [Fact]
        public void LessThanDoubles_ActualEqual_LimitNotBroken()
        {
            double limit = 1d;
            double actual = 1d;

            Assert.False(sut.LimitBroken(limit, actual));
        }

        [Fact]
        public void LessThanDoubles_ActualGreater_LimitNotBroken()
        {
            double limit = 1d;
            double actual = 10d;

            Assert.False(sut.LimitBroken(limit, actual));
        }

        [Fact]
        public void LessThanDoubles_ActualLess_LimitBroken()
        {
            double limit = 10d;
            double actual = 1d;

            Assert.True(sut.LimitBroken(limit, actual));
        }
    }
}
