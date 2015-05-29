using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BMonitor.Common.Models
{
    class EvaluationOperationTest
    {
        [Fact]
        public void CanConvertEvaluationOperationToString()
        {
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(EvaluationOperation));

            Assert.True(tc.CanConvertTo(typeof(string)));
        }

        //[Fact]
        //public void ConvertEvaluationOperationToString()
        //{
        //    EvaluationOperation eo = new EvaluationOperation { { X = 100, Y = 200 }};

        //    TypeConverter tc = TypeDescriptor.GetConverter(typeof(Point));

        //    Assert.Equal("(100,200)", tc.ConvertTo(p, typeof(string)));
        //}

        [Fact]
        public void CanConvertStringToEvaluationOperation()
        {
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(EvaluationOperation));

            Assert.True(tc.CanConvertFrom(typeof(string)));
        }

        [Fact]
        public void ConvertStringToGreaterThan()
        {
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(EvaluationOperation));

            EvaluationOperation eo = (EvaluationOperation)tc.ConvertFrom("greaterthan");
            Assert.Equal(eo.GetType(), typeof(GreaterThan));
        }

        [Fact]
        public void ConvertStringToLessThan()
        {
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(EvaluationOperation));

            EvaluationOperation eo = (EvaluationOperation)tc.ConvertFrom("lessthan");
            Assert.Equal(eo.GetType(), typeof(LessThan));
        }
    }
}
