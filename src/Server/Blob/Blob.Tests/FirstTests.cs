using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Blob.Tests
{
    public class FirstTests
    {

        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, 2 + 2);
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, 2 + 2);
        }
    }
}
