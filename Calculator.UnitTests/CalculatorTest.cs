using Xunit;

namespace Calculator.UnitTests
{
    public class CalculatorTest
    {
        [Theory]
        [InlineData("1 + 1", 2)]
        [InlineData("2 * 2", 4)]
        [InlineData("1 + 2 + 3", 6)]
        [InlineData("6 / 2", 3)]
        [InlineData("11 + 23", 34)]
        [InlineData("11.1 + 23", 34.1)]
        [InlineData("1 + 1 * 3", 6)]
        [InlineData("( 11.5 + 15.4 ) + 10.1", 37)]
        [InlineData("23 - ( 29.3 - 12.5 )", 6.2)]
        [InlineData("10 - ( 2 + 3 * ( 7 - 5 ) )", 0)]
        public void CalculateShouldReturnExpectedValueFromExpression(string input, double expectedResult)
        {
            var service = new Service();
            var result = service.Calculate(input);
            Assert.Equal(expectedResult, result);
        }
    }
}
