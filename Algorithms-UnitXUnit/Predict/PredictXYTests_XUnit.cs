using Algorithms.Algorithms.Predict;
using Xunit;
using Algorithms.Algorithms.Predict.Implementations;

namespace Algorithms_Unit
{
    public class PredictXYTests_XUnit
    {
        private readonly IPredict sut;

        public PredictXYTests_XUnit()
        {
            sut = new PredictXY();
        }
    
        [Fact(Skip = "Implement")]
        public void PredictXY()
        {
            var x = 1;
            var y = 1;

            var result = sut.Predict(x, y);

            Assert.True(result > 0);
        }
    }
}
