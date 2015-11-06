using Xunit;

namespace BMonitor.Common.Operations
{
    public class NotEqualTests
    {
        private NotEqual sut = new NotEqual();

        [Fact]
        public void NotEqualDoubles_ActualEqualInt_LimitNotBroken()
        {
            int limit = 1;
            int actual = 1;

            Assert.False(sut.LimitBroken(limit, actual));
        }

        [Fact]
        public void NotEqualDoubles_ActualLessInt_LimitBroken()
        {
            int limit = 10;
            int actual = 1;

            Assert.True(sut.LimitBroken(limit, actual));
        }

        [Fact]
        public void NotEqualDoubles_ActualGreaterInt_LimitBroken()
        {
            int limit = 1;
            int actual = 10;

            Assert.True(sut.LimitBroken(limit, actual));
        }

        [Fact]
        public void NotEqualDoubles_ActualEqual_LimitNotBroken()
        {
            double limit = 1d;
            double actual = 1d;

            Assert.False(sut.LimitBroken(limit, actual));
        }

        [Fact]
        public void NotEqualDoubles_ActualLess_LimitBroken()
        {
            double limit = 10d;
            double actual = 1d;

            Assert.True(sut.LimitBroken(limit, actual));
        }

        [Fact]
        public void NotEqualDoubles_ActualGreater_LimitBroken()
        {
            double limit = 1d;
            double actual = 10d;

            Assert.True(sut.LimitBroken(limit, actual));
        }
    }
}
