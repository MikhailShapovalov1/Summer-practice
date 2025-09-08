using task14;

namespace task14tests
{
    public class DefiniteIntegralTests
    {
        [Fact]
        public void TestLinearFunction()
        {
            var X = (double x) => x;
            Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, X, 1e-4, 2), 1e-4);
        }

        [Fact]
        public void TestSinFunction()
        {
            var sin = (double x) => Math.Sin(x);
            Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, sin, 1e-5, 8), 1e-4);
        }

        [Fact]
        public void TestAnotherLinearFunction()
        {
            var X = (double x) => x;
            Assert.Equal(12.5, DefiniteIntegral.Solve(0, 5, X, 1e-6, 8), 1e-5);
        }
    }
}
