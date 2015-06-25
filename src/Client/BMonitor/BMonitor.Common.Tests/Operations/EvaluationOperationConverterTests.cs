using System.ComponentModel;
using Xunit;

namespace BMonitor.Common.Operations
{
    public class EvaluationOperationConverterTests
    {
        private TypeConverter sut = TypeDescriptor.GetConverter(typeof(EvaluationOperation));

        [Fact]
        public void CanConvertEvaluationOperationToString()
        {
            Assert.True(sut.CanConvertTo(typeof(string)));
        }

        //[Fact]
        //public void CanConvertStringToEvaluationOperation()
        //{
        //    Assert.True(sut.CanConvertFrom(typeof(string)));
        //}

        [Fact]
        public void ConvertStringToGreaterThan()
        {
            EvaluationOperation eo = (EvaluationOperation)sut.ConvertFrom("greaterthan");
            Assert.NotNull(eo);
            Assert.Equal(eo.GetType(), typeof(GreaterThan));
        }

        [Fact]
        public void ConvertStringToLessThan()
        {
            EvaluationOperation eo = (EvaluationOperation)sut.ConvertFrom("lessthan");
            Assert.NotNull(eo);
            Assert.Equal(eo.GetType(), typeof(LessThan));
        }
    }
}
