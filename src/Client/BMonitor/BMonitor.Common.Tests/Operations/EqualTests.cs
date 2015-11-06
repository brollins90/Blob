using Xunit;

namespace BMonitor.Common.Operations
{
    public class EqualTests
    {
        private Equal sut = new Equal();

        [Fact]
        public void EqualDoubles_ActualEqualInt_LimitBroken()
        {
            int limit = 1;
            int actual = 1;

            Assert.True(sut.LimitBroken(limit, actual));
        }

        [Fact]
        public void EqualDoubles_ActualGreaterInt_LimitNotBroken()
        {
            int limit = 1;
            int actual = 10;

            Assert.False(sut.LimitBroken(limit, actual));
        }

        [Fact]
        public void EqualDoubles_ActualEqual_LimitBroken()
        {
            double limit = 1d;
            double actual = 1d;

            Assert.True(sut.LimitBroken(limit, actual));
        }

        [Fact]
        public void EqualDoubles_ActualGreater_LimitNotBroken()
        {
            double limit = 1d;
            double actual = 10d;

            Assert.False(sut.LimitBroken(limit, actual));
        }
    }
}
