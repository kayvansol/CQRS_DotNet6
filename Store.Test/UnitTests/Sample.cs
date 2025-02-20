using Store.Test.Data;
using Store.Test.UnitTests.Data;
using Xunit;

namespace Store.Test.UnitTests
{
    public class Sample
    {
        [Fact]
        public void Test1()
        {

            var calculator = new Calculator();

            int value1 = 1;
            int value2 = 2;

            var result = calculator.Add(value1, value2);

            Assert.Equal(3, result);

        }


        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-4, -6, -11)]
        [InlineData(-2, 2, 0)]
        [InlineData(int.MinValue, -1, int.MaxValue)]
        public void CanAddTheory(int value1, int value2, int expected)
        {
            var calculator = new Calculator();

            var result = calculator.Add(value1, value2);

            Assert.Equal(expected, result);
        }


        [Theory]
        [ClassData(typeof(CalculatorTestData))]
        public void CanAddTheoryClassData(int value1, int value2, int expected)
        {
            var calculator = new Calculator();

            var result = calculator.Add(value1, value2);

            Assert.Equal(expected, result);
        }


        [Theory]
        [MemberData(nameof(CalculatorTestData.Data), MemberType = typeof(CalculatorTestData))]
        public void CanAddTheoryMemberDataMethod(int value1, int value2, int expected)
        {
            var calculator = new Calculator();

            var result = calculator.Add(value1, value2);

            Assert.Equal(expected, result);
        }

    }

}