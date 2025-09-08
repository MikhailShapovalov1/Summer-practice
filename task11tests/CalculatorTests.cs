using task11;

namespace task11tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_CorrectAdd()
        {
            dynamic calculator = CalculatorGenerator.CreateCalculator();
            int result = calculator.Add(2, 3);
            Assert.Equal(5, result);
        }

        [Fact]
        public void Minus_CorrectSubstract()
        {
            dynamic calculator = CalculatorGenerator.CreateCalculator();
            int result = calculator.Minus(10, 7);
            Assert.Equal(3, result);
        }

        [Fact]
        public void Mul_CorrectMultiplicate()
        {
            dynamic calculator = CalculatorGenerator.CreateCalculator();
            int result = calculator.Mul(3, 4);
            Assert.Equal(12, result);
        }

        [Fact]
        public void Div_CorrectDivide()
        {
            dynamic calculator = CalculatorGenerator.CreateCalculator();
            int result = calculator.Div(6, 3);
            Assert.Equal(2, result);
        }

        [Fact]
        public void Div_ThrowsExceptionWhenDividingByZero()
        {
            dynamic calculator = CalculatorGenerator.CreateCalculator();
            Assert.Throws<DivideByZeroException>(() => calculator.Div(1, 0));
        }
    }
}
